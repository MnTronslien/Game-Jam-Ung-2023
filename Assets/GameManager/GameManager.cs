using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private List<PlayerInfo> players = new List<PlayerInfo>();
    [SerializeField] private PlayerScript playerPrefab;
    [SerializeField] private PlayerConfig[] playerConfigs;

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





        //Select a random level, but for now just load indext 2. NB: Remember to add the scene to the build settings
        await SceneManager.LoadSceneAsync(2);

        //Get spawn positions
        var spawnPositions = FindObjectsOfType<SpawnPosition>().ToList();
        //Scramble spawn positions
        spawnPositions = spawnPositions.OrderBy(x => Guid.NewGuid()).ToList();

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
            Debug.Log($"Player {p.config.name} is alive: {p.IsAlive()}");
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
        Debug.Log("GameManager: UpdateUI");
        foreach (var p in players)
        {
            Debug.Log($"Score for player {p.config.name} is {p.score}");
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

    internal class PlayerInfo
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
