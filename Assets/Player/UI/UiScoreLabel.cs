using UnityEngine;
using Image = UnityEngine.UI.Image;
public class UiScoreLabel : MonoBehaviour {

    public TMPro.TextMeshProUGUI textMesh;
    //Unity ui image component
    public Image deathIcon;
    private Color playerColour;


    void Start() {
        deathIcon.gameObject.SetActive(false);
    }

    public void SetScore(int amount) {
        textMesh.text = amount.ToString();
    }

    internal void SetColor(Color color)
    {
        playerColour = color;
        textMesh.color = color;
    }

    public void SetDead(bool isDead)
    {
        textMesh.color = isDead ? Color.Lerp(textMesh.color, Color.white, 0.5f) : playerColour;           
        deathIcon.gameObject.SetActive(isDead);
    }
}