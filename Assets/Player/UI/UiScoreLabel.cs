using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
public class UiScoreLabel : MonoBehaviour {

    public TMPro.TextMeshProUGUI textMesh;
    //Unity ui image component
    public Image deathIcon;

    void Start() {
        deathIcon.gameObject.SetActive(false);
    }

    public void SetScore(int amount) {
        textMesh.text = amount.ToString();
    }

    internal void SetColor(Color color)
    {
        textMesh.color = color;
    }

    public void SetDead(){
        textMesh.color = Color.Lerp(textMesh.color, Color.white, 0.5f);
        deathIcon.gameObject.SetActive(true);
    }
}