using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<PlayerScript> players = new List<PlayerScript>();
    public Guid id = Guid.NewGuid();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayer(PlayerScript player) {
        players.Add(player);
        Debug.Log($"Added player to {players} in ID {id}, currently at length {players.Count}");
    }

    public void PlayerHealthReached0(PlayerScript player) {
        int idx = players.IndexOf(player);
        Debug.Log($"Got player {idx}");
    }
}
