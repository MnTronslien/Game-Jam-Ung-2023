using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{

    public int winningScore = 15;
    public static GameManager Instance;
    private List<PlayerInfo> players = new List<PlayerInfo>();
    [SerializeField] private PlayerScript playerPrefab;
    [SerializeField] private PlayerConfig[] playerConfigs;
    public GameObject backgroundPrefab;

    //UI score script prefab
    public UiScoreLabel uiScoreLabelPrefab;

    private int numberOfPlayers = 2;
    public async void StartGame(int numOfPlayers)
    {
        numberOfPlayers = numOfPlayers;

        //Set up players, but dont load any levels or spawn anything yet
        for (int i = 0; i < numOfPlayers; i++)
        {
            //Create a new player info
            var newPlayer = new PlayerInfo
            {
                config = playerConfigs[i],
                score = 0,
            };

            newPlayer.uiScoreLabel = Instantiate(uiScoreLabelPrefab, uiScoreLabelPrefab.transform.position, Quaternion.identity);
            newPlayer.uiScoreLabel.transform.SetParent(uiScoreLabelPrefab.transform.parent);
            newPlayer.uiScoreLabel.SetColor(newPlayer.config.color);
            newPlayer.uiScoreLabel.SetScore(0);
            newPlayer.uiScoreLabel.gameObject.SetActive(true);
            players.Add(newPlayer);
        }
        uiScoreLabelPrefab.gameObject.SetActive(false);




        await LoadNewRandomLevelAsync(numOfPlayers);

        

    }

    private async Task LoadNewRandomLevelAsync(int numOfPlayers)
    {
        //Select a random level but exlude the first 2 levels and the last level
        var levelIndex = UnityEngine.Random.Range(2, SceneManager.sceneCountInBuildSettings-1);
        //GEt name of level
        var levelName = SceneUtility.GetScenePathByBuildIndex(levelIndex);
        Debug.Log($"Loading level {levelName} with index {levelIndex}");
        await SceneManager.LoadSceneAsync(levelIndex);

        //Get spawn positions
        var spawnPositions = FindObjectsOfType<SpawnPosition>().ToList();
        //Scramble spawn positions
        spawnPositions = spawnPositions.OrderBy(x => Guid.NewGuid()).ToList();

        Assert.IsTrue(spawnPositions.Count >= numOfPlayers, "Not enough spawn positions for the number of players");

        //Spawn players
        for (int i = 0; i < numOfPlayers; i++)
        {
            var player = Instantiate(playerPrefab, spawnPositions[i].transform.position, Quaternion.identity);
            player.config = playerConfigs[i];
            player.playerListIndex = i;
            players[i].playerGameObject = player; //Assign player game object to player info
        }
        //Clean up the spawn position game objects
        foreach (var spawnPosition in spawnPositions)
        {
            Destroy(spawnPosition.gameObject);
        }

        //Spawn background
        Instantiate(backgroundPrefab, Vector3.forward*100, Quaternion.identity);
    }

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
        DontDestroyOnLoad(uiScoreLabelPrefab.transform.root);
    }

    void Start()
    {
        uiScoreLabelPrefab.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayerHealthReached0(PlayerScript player)
    {
        //When a player dies, we need to add score to all the other players
        Debug.Log("GameManager: PlayerHealthReached0");
        foreach (var p in players)
        {
            if (p.IsAlive())
            {
                p.score += 1;
            }
        }

        //Update UI
        UpdateUI(players);

        //Then if there are only one player alive, we declare a winner.
        if (players.Count(p => p.IsAlive()) == 1)
        {
            //Give living player 1 point
            var winner = players.First(p => p.IsAlive());
            winner.score += 1;

            //A player has enough score to win, we end the game, if not we load a new level
            //CHEck all players
            if (players.Any(p => p.score >= winningScore))
            {
                //End game
                FinishRound();
            }
            else
            {
                //Load new level
                LoadNewRandomLevelAsync(numberOfPlayers);
            }


        }

        //Pause game
        //SHow UI for winning player - this should be another scene with podium and stuff
    }

    public PlayerInfo Winner => players.OrderByDescending(p => p.score).First();

    public List<PlayerInfo> Podium => players.OrderByDescending(p => p.score).ToList();

    private void FinishRound()
    {
        SceneManager.LoadScene("WinScene"); //Win scene has an animator script which will show the winner
    }

    private void UpdateUI(List<PlayerInfo> players)
    {
        //FOR each player in players, set the score in the UI
        Debug.Log("GameManager: UpdateUI");
        foreach (var p in players)
        {
            p.uiScoreLabel.SetScore(p.score);
        }
    }


    //Contect menu item to kill a ransom living player
    [ContextMenu("Kill a random player")]
    public void KillRandomPlayer()
    {
        var livingPlayers = players.Where(p => p.IsAlive()).ToList();
        if (livingPlayers.Count > 0)
        {
            var randomPlayer = livingPlayers[UnityEngine.Random.Range(0, livingPlayers.Count)];
            randomPlayer.playerGameObject.TakeDamage(100);
        }
    }

    public class PlayerInfo
    {
        public PlayerScript playerGameObject;

        public PlayerConfig config;

        public UiScoreLabel uiScoreLabel;

        public int score = 0;

        public bool IsAlive()
        {
            if (playerGameObject == null)
            {
                return false;
            }
            return playerGameObject.health > 0;
        }
    }
}
