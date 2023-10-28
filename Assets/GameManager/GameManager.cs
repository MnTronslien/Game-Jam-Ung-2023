using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<string> PlayerNames = new List<string>();
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Destroy(this);
        else { 
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
