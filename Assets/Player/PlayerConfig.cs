//Scriptable object for player config like colour and copntrol scheme


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig", order = 1)]
public class PlayerConfig : ScriptableObject
{
    public string playerName = "Johnny";
    public Color color = Color.white;
    public KeyCode leftThrusterKey = KeyCode.A;
    public KeyCode rightThrusterKey = KeyCode.D;
    public KeyCode thrustKey = KeyCode.S;
}