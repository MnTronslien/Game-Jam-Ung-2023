using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
public class UiScoreLabel : MonoBehaviour {

    public TMPro.TextMeshProUGUI textMesh;

    public void SetScore(int amount) {
        textMesh.text = amount.ToString();
    }

    internal void SetColor(Color color)
    {
        textMesh.color = color;
    }
}