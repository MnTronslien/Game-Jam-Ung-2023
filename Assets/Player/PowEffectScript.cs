using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowEffectScript : MonoBehaviour
{
    public float LongLast = 3f;
    private float timer = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>LongLast) Destroy(gameObject);
    }
}
