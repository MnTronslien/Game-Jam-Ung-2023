using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private List<PlayerInfo> players = new List<PlayerInfo>();

    //UI score script prefab
    public UiScoreLabel uiScoreLabelPrefab;



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

    void Start()
    {

        uiScoreLabelPrefab.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPlayer(PlayerScript player)
    {
        var newPlayer = new PlayerInfo()
        {
            player = player,
            color = player.playerColor,
            uiScoreLabel = Instantiate(uiScoreLabelPrefab, uiScoreLabelPrefab.transform.parent)
        };
        newPlayer.uiScoreLabel.SetColor(newPlayer.color);
        newPlayer.uiScoreLabel.SetScore(0);
        newPlayer.uiScoreLabel.gameObject.SetActive(true);

        players.Add(newPlayer);


        //move player ui to the right place

    }

    public void PlayerHealthReached0(PlayerScript player)
    {
        //When a player dies, we need to add score to all the other players

        foreach (var p in players)
        {
            if (p.IsAlive)
            {
                p.score += 1;
            }
        }

        //Update UI
        UpdateUI(players);

        //Then if there are only one player alive, we declare a winner.
        if (players.Count(p => p.IsAlive) == 1)
        {
            //Give living player 1 point
            var winner = players.First(p => p.IsAlive);
            winner.score += 1;

            SceneManager.LoadScene("WinScene"); //Win scene has an animator script which will show the winner
        }

        //Pause game
        //SHow UI for winning player - this should be another scene with podium and stuff
    }

    private void FinishRound()
    {
        throw new NotImplementedException();
    }

    private void UpdateUI(List<PlayerInfo> players)
    {
        //FOR each player in players, set the score in the UI
        foreach (var p in players)
        {
            p.uiScoreLabel.AddScore(p.score);
        }
    }

    internal class PlayerInfo
    {
        public PlayerScript player;

        public UiScoreLabel uiScoreLabel;

        public int score = 0;

        public Color color;

        public bool IsAlive => player.health > 0;

    }

}
