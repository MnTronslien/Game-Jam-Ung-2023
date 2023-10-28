using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<PlayerScript> players = new List<PlayerScript>();

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

    public int AddPlayer(PlayerScript player) {
        players.Append(player);
        return players.Count;
    }

    public void PlayerHealthReached0(PlayerScript player) {
        int idx = players.IndexOf(player);
        Debug.Log($"Got player {idx}");
    }
}
