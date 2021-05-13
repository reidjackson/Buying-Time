using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitScript : MonoBehaviour
{


    
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
            Debug.Log("quit");
        }
    }
}
