using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    

    public Transform canvas;
    //public PlayerController Player;
	
	void Update () {

	    if (Input.GetKeyDown(KeyCode.Escape)){
            if (canvas.gameObject.activeInHierarchy == false){
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                //Player = FindObjectOfType<PlayerController>();
                //Player.gameObject.active = false;            
            }
            else{
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                print("ldas");
                //Player.gameObject.active = true;       
            }
        }	
	}
    
    public void CloseOnResume() 
    {
        if (canvas.gameObject.activeInHierarchy == true){
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
