using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    

    public Transform canvas;
	
	void Update () {

	    if (Input.GetKeyDown(KeyCode.Escape)){
            if (canvas.gameObject.activeInHierarchy == false){
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                PlayerController.Instance.enabled = false;
                MouseAimCamera.Instance.enabled = false;
            }
            else{
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                print("ldas");
                PlayerController.Instance.enabled = true;
                MouseAimCamera.Instance.enabled = true;
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
