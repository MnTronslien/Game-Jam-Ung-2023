using System;
using Unity.VisualScripting;
using UnityEngine;
public class UiScoreLabel : MonoBehaviour {

    public TMPro.TextMeshProUGUI textMesh;
    public int score = 0;

    public void AddScore(int amount) {
        score += amount;
        textMesh.text = score.ToString();
    }
    public void SetScore(int amount) {
        score = amount;
        textMesh.text = score.ToString();
    }

    internal void SetColor(Color color)
    {
        textMesh.color = color;
    }
}