using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeBetween = 200f;
    public GameObject slime;
    private float randF = 0f;
    public float targetSpawn = 90f;
    private CharacterScript CharacterScript;

    void Start()
    {
        timeBetween = Random.Range(0, 500);
        CharacterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CharacterScript.dead) {
            timeBetween--;

            if (timeBetween < 0)
            {
                randF = Random.Range(0f, 100f);

                if (randF > targetSpawn) {

                    Instantiate(slime, transform.position, transform.rotation);
                    Debug.Log(randF);

                }

                timeBetween = 500;
            }
        }
    }
}
