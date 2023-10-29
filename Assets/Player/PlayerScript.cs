using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerScript : MonoBehaviour
{

    public PlayerNumber playerNumber;

    public PlayerConfig config;

    //Stats player health
    public int health = 100;

    float leftInput;
    float rightInput;

    float thrust;
    public float thrustMultiplier = 10f;

    public int playerListIndex;


    public Rigidbody2D rb;
    public GameObject PowEffect;
    public GameObject leftThruster;
    public GameObject rightThruster;
    

    //Visuals
    public ParticleSystem leftThrusterParticles;
    public ParticleSystem rightThrusterParticles;

    public Guid id = Guid.NewGuid();

    public float CoolDown = 0f;

    private TrailRenderer trailRenderer;

    [Header("Audio")]
    public List<AudioClip> grunts;

    [Header("Audio")]
    public List<AudioClip> damageGrunts;

    public enum PlayerNumber
    {
        Player1,
        Player2,
        Player3,
    }

    void Start()
    {
        //Set player color
        GetComponent<SpriteRenderer>().color = config.color;
        GetComponent<TrailRenderer>().startColor = config.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (CoolDown > 0f) CoolDown -= Time.deltaTime;
        if (transform.position.y < -100f) TakeDamage(9999);
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
                rb.angularVelocity = 0f;//resets the angular velocity
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
                rb.angularVelocity = 0f;
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
        if (thrust > 0 && CoolDown <= 0)//hvis spilleren ble truffet så kan de ikke booste til cooldownen er over
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
        leftInput = Input.GetKey(config.leftThrusterKey) ? 1 : 0;
        rightInput = Input.GetKey(config.rightThrusterKey) ? 1 : 0;
        thrust = Input.GetKey(config.thrustKey) ? 1 : 0;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            Debug.Log(gameObject.name + " hit " + other.gameObject.name);
            PlayerScript ColliderScript = other.gameObject.GetComponent<PlayerScript>();



                //denne har størst fart
            if(rb.velocity.magnitude*thrust > other.rigidbody.velocity.magnitude*ColliderScript.thrust && CoolDown <= 0 && ColliderScript.CoolDown <= 0){
                
                
                //BUG: Is never reached
                Debug.Log("This player should not grunt");

                rb.angularVelocity = 1f;
                ColliderScript.CoolDown = 0.3f;
                other.rigidbody.velocity = rb.velocity*2;
                rb.velocity /= -2;
                Instantiate(PowEffect, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity);
                JuiceManager.PauseFor(2000f);
            }else if(rb.velocity.magnitude*thrust < other.rigidbody.velocity.magnitude*ColliderScript.thrust && CoolDown <= 0 && ColliderScript.CoolDown <= 0){

                Debug.Log("the Player wo has most velocity shall now grunt");
                var grunt = grunts[UnityEngine.Random.Range(0, grunts.Count)];
                DropSoundManager.Instance.PlayDropSound(grunt, varyPitch:true);


                other.rigidbody.angularVelocity = 0f;
                CoolDown = 1f;
                rb.velocity = other.rigidbody.velocity*2;
                other.rigidbody.velocity /= -2;
                Instantiate(PowEffect, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity);
            }
        }
    }
    public void TakeDamage(int amount)
    {
        var grunt = damageGrunts[UnityEngine.Random.Range(0, damageGrunts.Count)];
        DropSoundManager.Instance.PlayDropSound(grunt, varyPitch:true);

        health -= amount;
        if (health <= 0)
        {
            GameManager.Instance.PlayerHealthReached0(this);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
