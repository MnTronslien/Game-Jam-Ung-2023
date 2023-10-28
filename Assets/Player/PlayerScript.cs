using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerScript : MonoBehaviour
{

    public PlayerNumber playerNumber;
    public Color playerColor = Color.white;

    //Stats player health
    public int health = 100;

    float leftInput;
    float rightInput;

    float thrust;
    public float thrustMultiplier = 10f;

    public int playerListIndex;


    public Rigidbody2D rb;
    public GameObject leftThruster;
    public GameObject rightThruster;

    public KeyCode leftThrusterKey = KeyCode.A;
    public KeyCode rightThrusterKey = KeyCode.D;
    public KeyCode thrustKey = KeyCode.S;



    //Visuals
    public ParticleSystem leftThrusterParticles;
    public ParticleSystem rightThrusterParticles;

    public Guid id = Guid.NewGuid();



    public enum PlayerNumber
    {
        Player1,
        Player2,
        Player3,
    }

    void Start()
    {
        //Set player color
        GetComponent<SpriteRenderer>().color = playerColor;
        GameManager.Instance.AddPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        ActOnInput();
    }

    private void ActOnInput()
    {
        if (leftInput > 0)
        {
            if (thrust > 0){//resets the angular velocity
                rb.angularVelocity = 0f;
                rb.rotation += 1 * Time.deltaTime * 100;
            }else{
                rb.rotation += 2 * Time.deltaTime * 100;
            }
            
            //Set thruster local rotation to be 90 degrees to the right
            rightThruster.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            //Set thruster local rotation to be 0 degrees
            rightThruster.transform.localRotation = Quaternion.Euler(0, 0, 30);
        }
        if (rightInput > 0)
        {
            if (thrust > 0){
                rb.angularVelocity = 0f;//resets the angular velocity
                rb.rotation -= 1 * Time.deltaTime * 100;
            }else{
                rb.rotation -= 2 * Time.deltaTime * 100;
            }
            //Set thruster local rotation to be 90 degrees to the right
            leftThruster.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            //Set thruster local rotation to be 0 degrees
            leftThruster.transform.localRotation = Quaternion.Euler(0, 0, -30);
        }
        if (thrust > 0)
        {
            //If thrusting add for ce from both thrusters in their respective directions
            rb.AddForce(leftThruster.transform.up * thrustMultiplier * Time.deltaTime);
            rb.AddForce(rightThruster.transform.up * thrustMultiplier * Time.deltaTime);

            //Play particles
            leftThrusterParticles.Play();
            rightThrusterParticles.Play();
        }
        else
        {
            //Stop particles
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }
    }

    private void GetInput()
    {
        leftInput = Input.GetKey(leftThrusterKey) ? 1 : 0;
        rightInput = Input.GetKey(rightThrusterKey) ? 1 : 0;
        thrust = Input.GetKey(thrustKey) ? 1 : 0;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            GameManager.Instance.PlayerHealthReached0(this);
            Die();
        }
    }

    private void Die()
    {
        //TODO: Impelemnt any death animations and object despawn.
    }
}
