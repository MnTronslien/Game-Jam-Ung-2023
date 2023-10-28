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
    private static GameManager _instance;

    public static GameManager Instance;
    private List<PlayerInfo> players = new List<PlayerInfo>();

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
        players.Add(new PlayerInfo() {
            player = player,
            color = player.playerColor
        });
    }

    public void PlayerHealthReached0(PlayerScript player) {
        //When a player dies, we need to add score to all the other players

        foreach (var p in players) {
            if (p.IsAlive) {
                p.score += 1;
            }
        }

        //Update UI
        UpdateUI();

        //Then if there are only onle player alive, we declare a winner.
            //Pause game
            //SHow UI for winning player - this should be another scene with podium and stuff

    }

    private void UpdateUI()
    {
        throw new NotImplementedException();
    }

    private class PlayerInfo
    {
        public PlayerScript player;

        public int score = 0;

        public Color color;

        public bool IsAlive => player.health > 0;
  
    }

}
