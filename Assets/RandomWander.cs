using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class RandomWander : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame

    float cooldown = 0;


    void Update()
    {
        //If cooldown is 0, do something
        if (cooldown <= 0)
        {
            //Do something
            cooldown = 10f;
            Moved();
        }
        else
        {
            //Count down cooldown
            cooldown -= Time.deltaTime;
        }
    }

    private void Moved()
    {
        //Add a small random foce to the object
        GetComponent<Rigidbody2D>().AddForce(UnityEngine.Random.insideUnitCircle * 100f);
        //Add a random angular velocity to the object
        GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-100f, 100f);
    }
}
