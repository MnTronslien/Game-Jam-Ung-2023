using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseHoverClick : MonoBehaviour
{
    public SpriteRenderer thisrenderer;
    public bool QuitButton = false;
    void OnMouseEnter(){
        thisrenderer.color = Color.red;
        Debug.Log("Mouse enter");
    }
    void OnMouseExit() {
        thisrenderer.color = Color.white;
        Debug.Log("Mouse leave");
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)){
            switch (QuitButton){
                case true:
                    Debug.Log("Game quit!");
                    break;
                case false:
                    Debug.Log("Button Clicked!");
                    SceneManager.LoadScene("SampleScene");
                    break;
            }
            
        }
    }

}
