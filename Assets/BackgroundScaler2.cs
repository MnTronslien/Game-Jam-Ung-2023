using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BackgroundScaler2 : MonoBehaviour
{
    async void Start()
    {
        //Wait untill the Camera is ready
        await UniTask.WaitUntil(() => Camera.main != null);
        //Get the camera
        Camera cam = Camera.main;
        //Get the size of the camera
        float height = cam.orthographicSize / 2;

        transform.localScale = Vector3.one * height;

    }
}
