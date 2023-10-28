using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelRotation : MonoBehaviour
{
    public float TargetSpeed = 2f;
    [SerializeField] private float realSpeed = 0f;
    [SerializeField] private float waitfor = 0f;
    public int Creativity = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (waitfor <= 0f) {
            waitfor = UnityEngine.Random.Range(20f/Creativity, 200f/Creativity);
            TargetSpeed = (float)Math.Round(UnityEngine.Random.Range(-10f*Creativity/3, 10f*Creativity/3),3);
        }else{
            waitfor -= Time.deltaTime;
        }
        
        if (realSpeed < TargetSpeed){
            realSpeed += (float)Math.Round(0.01f + realSpeed/2f*Time.deltaTime,3);
        }else if(realSpeed > TargetSpeed){
            realSpeed -= (float)Math.Round(0.01f + realSpeed/2f*Time.deltaTime,3);
        }
        
        if(math.abs(realSpeed) - math.abs(TargetSpeed) > 0.2f || math.abs(realSpeed) - math.abs(TargetSpeed) < 0.2f) transform.Rotate(new Vector3(0, 0, realSpeed * Time.deltaTime));
    }
}