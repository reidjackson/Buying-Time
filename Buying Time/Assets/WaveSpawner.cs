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

    // a set is 3 waves
    private int setNumber = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 20f;
    public float waveCountdown;

    private float searchCounter = 4f;

    private SpawnState state = SpawnState.COUNTING;

    void Start() {
        waveCountdown = timeBetweenWaves;

        if(spawnPoints.Length == 0) {
            Debug.Log("NO SPAWNPOINTS");
        }
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
            setNumber++;
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
        for (int i = 0; i < _wave.count + Mathf.FloorToInt((setNumber * _wave.count)); i++) {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy) {
        //Spawn an enemy
        Transform _spawnpoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(_enemy, _spawnpoint.position, _spawnpoint.rotation);
        Debug.Log("Spawning Enemy:" + _enemy.name);
    }
}
