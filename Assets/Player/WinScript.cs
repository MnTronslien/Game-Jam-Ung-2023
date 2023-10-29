using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Callbacks;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    public Rigidbody2D rb;
    private AudioSource audioSource;
    public AudioClip[] grunt;
    private AudioClip gruntclip;
    public SpriteRenderer bg;
    private SpriteRenderer sp;
    public GameObject target;
    public float Force = 1000f;
    public int occur = 500;
    private Vector2 facevector;
    private float cooldown = 0f;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        facevector = new Vector2(transform.position.x, transform.position.y+6);
        var winner = GameManager.Instance.Winner;
        sp.color = winner.config.color; bg.color = winner.config.color;
        Debug.Log(winner);
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f && rb.velocity.magnitude < 40f){
            Debug.Log("Jump!" + transform.rotation.z);
            rb.AddForceAtPosition(Force * (target.transform.position - transform.position).normalized, facevector);
            cooldown = Random.Range(0.5f, 1.5f);

            int index = Random.Range(0, grunt.Length);
            gruntclip = grunt[index];

            audioSource.clip = gruntclip;
            audioSource.Play();
        }
    }
}
