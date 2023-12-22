using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BirdScript : MonoBehaviour
{
    public Rigidbody2D birdRigidBody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdAlive = true;
    public float topScreen = 15.5f;
    public float botScreen = -14.5f;
    public float valorIfMicrofono = 0.1f;    
    private AudioSource birdFlapSound;
    AudioSource audioSource;
    public string selectedDevice; // Default Mic
    public static float[] samples = new float[128]; // Block for audioSource.GetOutputData()

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();  
        //birdFlapSound = GetComponent<AudioSource>();
        //-----------------------------------------------------------------
        audioSource = GetComponent<AudioSource>();

        if (Microphone.devices.Length > 0)
        {
            selectedDevice = Microphone.devices[0].ToString();
            logic.setMicrophoneText(selectedDevice);
            audioSource.clip = Microphone.Start(selectedDevice, true, 1, AudioSettings.outputSampleRate);
            audioSource.loop = true;

            /**
             * While the position of the mic in the recording is greater than 0,
             * play the clip (that should be the mic)
             */
            while (! (Microphone.GetPosition(selectedDevice) > 0) )
            {
                audioSource.Play();
            }
        }  
    }

    // Update is called once per frame
    void Update()
    {
        /* this is for mobile
           if (Input.touchCount > 0 && birdAlive)
           {
                birdRigidBody.velocity = Vector2.up * flapStrength;
                birdFlapSound.Play();
           }
        */
        /*  
           if (Input.GetKeyDown(KeyCode.Space) && birdAlive)
           {
                birdRigidBody.velocity = Vector2.up * flapStrength;
                birdFlapSound.Play();
           }
        */ 
           if(transform.position.y > topScreen || transform.position.y < botScreen)
           {
                gameOver();
           }

           getOutputData();
          
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

    void getOutputData()
    {
        audioSource.GetOutputData(samples, 0);

        float vals = 0.0f;

        for (int i=0; i<128; i++)
        {
            vals += Mathf.Abs(samples[i]);
        }
        vals /= 128.0f;        
        
        if (vals*20.0f>valorIfMicrofono && birdAlive)
        {            
            birdRigidBody.velocity = Vector2.up * flapStrength;    
        }
    }
}
