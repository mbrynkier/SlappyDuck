using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D birdRigidBody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdAlive = true;
    public float topScreen = 15.5f;
    public float botScreen = -14.5f;
    private AudioSource birdFlapSound;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();  
        birdFlapSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
           if (Input.touchCount > 0 && birdAlive)
           {
                birdRigidBody.velocity = Vector2.up * flapStrength;
                birdFlapSound.Play();
           }
        */
            
           if (Input.GetKeyDown(KeyCode.Space) && birdAlive)
           {
                birdRigidBody.velocity = Vector2.up * flapStrength;
                birdFlapSound.Play();
           }
          
           if(transform.position.y > topScreen || transform.position.y < botScreen)
           {
                gameOver();
           }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        gameOver();
    }

    private void gameOver()
    {
        logic.gameOver();
        birdAlive = false;
    }
}
