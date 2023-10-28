using UnityEngine;

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


    public Rigidbody2D rb;
    public GameObject leftThruster;
    public GameObject rightThruster;

    public KeyCode leftThrusterKey = KeyCode.A;
    public KeyCode rightThrusterKey = KeyCode.D;
    public KeyCode thrustKey = KeyCode.S;



    //Visuals
    public ParticleSystem leftThrusterParticles;
    public ParticleSystem rightThrusterParticles;



    public enum PlayerNumber
    {
        Player1,
        Player2,
        Player3,
    }

    void Start(){
        //Set player color
        GetComponent<SpriteRenderer>().color = playerColor;
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
            rb.rotation += 1 * Time.deltaTime * 100;
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
            rb.rotation -= 1 * Time.deltaTime * 100;
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
}
