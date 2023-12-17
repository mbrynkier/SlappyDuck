using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPipeScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 2;
    private float timer;
    public float offset = 10;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }else
        {
            SpawnPipe();   
            timer = 0;        
        }
    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - offset;
        float highestPoint = transform.position.y + offset;

        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint,highestPoint),0), transform.rotation);
    }
}
