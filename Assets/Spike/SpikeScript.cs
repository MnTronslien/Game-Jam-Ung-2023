using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public float force = 100000;
    public int damage = 10;
    public SpriteRenderer SpriteGlow, SpriteFaded;
    public float glowAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float sinus = (Mathf.Sin(Time.time)/2) + 0.5f; //0 - 1
        // Debug.Log($"time is: {Time.time}, and sinus is {sinus}");
        var newColor = SpriteGlow.color;
        newColor.a = sinus;
        SpriteGlow.color = newColor;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;
        Rigidbody2D otherRigidBody = other.GameObject().GetComponent<Rigidbody2D>();
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            PlayerScript playerScript = other.GetComponent<PlayerScript>();
            if (playerScript != null) {
                playerScript.TakeDamage(damage);
            }
            Debug.Log("Collided with player");
            otherRigidBody.AddForce(transform.up * Time.deltaTime * force);

        }
    }
}
