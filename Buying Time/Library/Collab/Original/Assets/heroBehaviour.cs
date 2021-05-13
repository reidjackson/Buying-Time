using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heroBehaviour : MonoBehaviour
{

    public float speed = 7;
    public int maxHealth = 100;
    public int currentHealth;
    public int paperMoney = 0;
    public PlayerHealthBar healthBar;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator anim;
    private float currentTime;
    private bool canTakeDamage = true;

    void Start(){
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

     // Update is called once per frame
	void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            takeDamage(10);
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (change!=Vector3.zero){
            moveCharacter();
            anim.SetFloat("moveX", change.x);
            anim.SetFloat("moveY", change.y);
            anim.SetBool("moving", true);
        }else{
            anim.SetBool("moving", false);

        } 

        
    }

    IEnumerator DamageDelay() {
        canTakeDamage = false;
        yield return new WaitForSeconds(1.5f);
        canTakeDamage = true;
    }

    // void OnCollisionEnter2D(Collision2D collision) {
    //     GameObject otherObj = collision.gameObject;
    //     Debug.Log("Collided with: " + otherObj);
    //     if(otherObj.tag == "Enemy") {
    //         takeDamage(15);
    //         StartCoroutine(DamageDelay()); 
    //     }
    // }
 
    void OnCollisionStay2D(Collision2D collision) {
        GameObject otherObj = collision.gameObject;
        Debug.Log("Collided with: " + otherObj);
        if(otherObj.tag == "Enemy") {
            if(canTakeDamage) {
                takeDamage(15);
                StartCoroutine(DamageDelay()); 
            }
        }
    }


    void moveCharacter(){

        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );

    }

    void takeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

 

    public void Heal(int healAmount) {
        Debug.Log("Healing");
        if(currentHealth + healAmount > maxHealth) {
            currentHealth = maxHealth;
        } else {
            currentHealth += healAmount;
        }
        Debug.Log(currentHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void buyUpgrade(string upgrade) {
        if(upgrade == "Speed") {
            if(paperMoney - 0 < 0) {
                return;
            } else {
                paperMoney -= 0;
                speed += 2;
            }
        }

        if(upgrade == "Health") {
            if(paperMoney - 20 < 0) {
                return;
            } else {
                paperMoney -= 20;
                maxHealth += 10;
                Heal(10);
                healthBar.SetMaxHealth(maxHealth + 10);
            }
        }
    }

}