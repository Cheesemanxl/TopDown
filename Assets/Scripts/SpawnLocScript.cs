using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeBetween = 200f;
    public GameObject slime;
    public float randF = 0f;
    public float targetSpawn = 0.5f;


    void Start()
    {
        timeBetween = Random.Range(0, 500);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.time);
        timeBetween--;

        if (timeBetween < 0)
        {
            randF = Random.Range(0f, 1f);
            if (randF > targetSpawn) {
                Instantiate(slime, transform.position, transform.rotation);
            }
            timeBetween = Random.Range(0, 500);
        }
    }
}
