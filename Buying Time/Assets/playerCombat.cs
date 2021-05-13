using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
 

    public Animator animator;
    public Animator enemyAnimator;

    public Transform hurtboxRight;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    bool waitActive = false; 
    public heroBehaviour player;
    

    void Start() {
        player = GetComponent<heroBehaviour>();
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){

            attack();
        }


    }

    void attack(){
        //PLAY ANIMATION
        animator.SetTrigger("swing");
        
        //detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hurtboxRight.position, attackRange, enemyLayers);



        //damage
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("we hit " + enemy.name);
            enemyAnimator.SetBool("isDead", true);
              

            if(!waitActive){
             StartCoroutine(Wait());   
            }
            Destroy(enemy.gameObject);
            player.Heal(10);
            int moneyToAdd = Random.Range(0, 4);
            moneyScript.money += moneyToAdd;
        }

    }


    void OnDrawGizmosSelected(){
        if(hurtboxRight == null){
            return;
        }

        Gizmos.DrawWireSphere(hurtboxRight.position, attackRange);
    }

    IEnumerator Wait(){
     waitActive = true;
     yield return new WaitForSeconds (1.0f);
     waitActive = false;
 }

}
