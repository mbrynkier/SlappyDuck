using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaScript : MonoBehaviour
{
     AudioSource audioSource;

    // Default Mic
    public string selectedDevice;

    // Block for audioSource.GetOutputData()
    public static float[] samples = new float[128];

    public float valorIf = 1;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        /**
         * if there are microphones,
         * select the default mic,
         * set the audioSource.clip to the default mic, looped, for 1 second, at the sampleRate
         * set loop to true
         */
        if (Microphone.devices.Length > 0)
        {
            selectedDevice = Microphone.devices[0].ToString();
            Debug.Log(selectedDevice);
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


    void Update()
    {
        getOutputData();
    }

    /**
     * Load the block samples with data from the audioSource output
     * Average the values across the size of the block.
     * vals is the volume of the mic, used to control block height
     * Block height represents candle flame getting larger
     */
    void getOutputData()
    {
        audioSource.GetOutputData(samples, 0);

        float vals = 0.0f;

        for (int i=0; i<128; i++)
        {
            vals += Mathf.Abs(samples[i]);
        }
        vals /= 128.0f;
        //Debug.Log(vals);
       
        //gameObject.transform.localScale = new Vector3(1.0f, 1.0f + (vals * 10.0f), 1.0f);
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f + (vals * 20.0f), 1.0f);
        
        if (vals*20.0f>valorIf)
        {
            Debug.Log(vals);    
        }
    }
}
