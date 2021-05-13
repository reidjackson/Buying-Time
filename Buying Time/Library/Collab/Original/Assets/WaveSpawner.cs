using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 6f;
    public float waveCountdown;

    private float searchCounter = 4f;

    private SpawnState state = SpawnState.COUNTING;

    void Start() {
        waveCountdown = timeBetweenWaves;
    }

    void Update() {
        if(state == SpawnState.WAITING) {
            if(!EnemyStillAlive()) {
                NewRound();
            } else {
                return;
            }
        }

        if(waveCountdown <= 0) {
            if(state != SpawnState.SPAWNING) {
                // Spawn a wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            } 
        } else {
            waveCountdown -= Time.deltaTime;
         }

    }

    void NewRound() {

        Debug.Log("Wave Done...");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1) {
            nextWave = 0;
            Debug.Log("Completed Waves... Restarting...");
        } else {
            nextWave++;
        }

        
    }

    bool EnemyStillAlive() {

        searchCounter -= Time.deltaTime;
        if(searchCounter <= 0) {
            searchCounter = 4f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null) {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        Debug.Log("Spawning Wave!:" + _wave.name);
        state = SpawnState.SPAWNING;

        //Spawn stuff
        for (int i = 0; i < _wave.count; i++) {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy) {
        //Spawn an enemy
        Instantiate(_enemy, transform.position, transform.rotation);
        Debug.Log("Spawning Enemy:" + _enemy.name);
    }
}
