using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements.Experimental;

public class LevelRotation : MonoBehaviour
{
    // rotation speed
    public Vector2 speedRange = new Vector2(30, 100);
    // rotation duration
    public Vector2 durationRange = new Vector2(2f, 10f);

    public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(0, 1));
    void Start()
    {
        NewRotationMethod();
    }

    // Update is called once per frame

    private async void NewRotationMethod()
    {
        while (true)
        {
            if (this == null) return;
            //Generate a random speed
            float rotationSpeed = UnityEngine.Random.Range(speedRange.x, speedRange.y);

            //Multiply with time since level load and cap it at a max
            rotationSpeed *= Time.timeSinceLevelLoad * 0.1f;
            rotationSpeed = Mathf.Min(rotationSpeed, speedRange.y*2);


            //50% chance to rotate clockwise or counter clockwise
            if (UnityEngine.Random.value > 0.5f)
            {
                rotationSpeed *= -1;
            }
            //Generate a random duration
            float rotationDuration = UnityEngine.Random.Range(durationRange.x, durationRange.y);
            await RotateObjectAsync(rotationDuration, rotationSpeed);
        }
    }

    private async UniTask RotateObjectAsync(float rotationDuration, float rotationSpeed)
    {
    
        float startTime = Time.time;

        while (Time.time - startTime < rotationDuration)
        {
            if (this == null) return;
            //modify speed based on the curve and the time elapsed
            float rotSpeed = rotationSpeed * curve.Evaluate((Time.time - startTime) / rotationDuration);
            transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }
}