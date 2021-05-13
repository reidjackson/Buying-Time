using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueKnight : Enemy
{

    public Transform target;
    public float chaseRadius;
    public Animator animator;

    //public float attackRadius;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        checkDistance();
    }


    void checkDistance(){

        if(Vector3.Distance(target.position, transform.position) <= chaseRadius){
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);
            animator.SetBool("startMoving", true);
            if(Vector3.Distance(target.position, transform.position) < 0.5){
                //dealDamage
                

            }

        }else{
            animator.SetBool("startMoving", false);
        }

    }

    //Detect collisions between the GameObjects with Colliders attached
    


}
