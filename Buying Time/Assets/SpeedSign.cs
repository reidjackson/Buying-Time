using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSign : MonoBehaviour
{   

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog; 
    public bool playerInRange;
    public heroBehaviour player;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        playerInRange = false;
        player = GetComponent<heroBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange) {
            dialogBox.SetActive(true);
            dialogText.text = dialog;
        } else {
           dialogBox.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.B) && playerInRange) {
            // player.buyUpgrade("Speed");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            playerInRange = false;
            dialogBox.SetActive(false);
        }   
    }
}
