using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class MouseHoverClick : MonoBehaviour
{
    public int numberOfPlayers = 2;
    public SpriteRenderer highlight;
    public SpriteRenderer thisrenderer;
    public bool QuitButton = false;
    void OnMouseEnter(){
        thisrenderer.color = Color.red;
        highlight.enabled = true;
        Debug.Log("Mouse enter");
    }
    void OnMouseExit() {
        highlight.enabled = false;
        thisrenderer.color = Color.white;
        Debug.Log("Mouse leave");
    }

    public void Update(){
        //If highlight is enabled, then rotate the highlight
        if (highlight.enabled){
            highlight.transform.Rotate(0,0,Time.deltaTime * 100f);
        }
    }
private bool isClicked = false;
    private async void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)){
            switch (QuitButton){
                case true:
                    Debug.Log("Game quit!");
                    break;
                case false:
                    Debug.Log("Button Clicked!");
                    //Simple guard to prevent double clicking
                    if (isClicked) return;
                    isClicked = true;
                    //load manager scene and tell game manager to start game
                    await SceneManager.LoadSceneAsync(1); // 1 is the index of the manager scene
                    GameManager.Instance.StartGame(numberOfPlayers); //Warning: this might be a race condition
                    break;
            }
            
        }
    }

}
