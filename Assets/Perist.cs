using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perist : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
