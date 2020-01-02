using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSpawnScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeBetween;
    public float maxTimeBetween;
    public float targetSpawn = 10f;
    private float randF = 0f;

    private bool home = true;
    private Vector2 homeLoc;

    public GameObject slime;
    private CharacterScript CharacterScript;




    void Start()
    {
        timeBetween = 0;

        //get the player script from the player game object in the scene
        CharacterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();

        //set initial spawn location
        homeLoc = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if character is not dead continue spawning slimes
        if (!CharacterScript.dead)
        {
            SpawnTimingLogic();
        }
    }

    //spawn slimes every X seconds determined by the DecideTimeBetween() function
    void SpawnTimingLogic()
    {
        //decrement time between
        timeBetween--;

        //if time between is less than 0
        if (timeBetween < 0)
        {
            //roll to spawn a slime or not
            randF = Random.Range(0f, 100f);

            //determine whether to spawn a slime based on the roll
            if (randF > targetSpawn)
            {
                //make a slime
                Instantiate(slime, transform.position, transform.rotation);
                //Change spawn location
                ChangeLoc();
            }

            //reset timeBetween based on the player's score
            DecideTimeBetween();
        }
    }

    //Change spawn time based on current score
    void DecideTimeBetween()
    {
        if (UIScript.score < 10)
        {
            timeBetween = maxTimeBetween;
        }

        if (UIScript.score >= 10)
        {
            timeBetween = maxTimeBetween / 1.5f;
        }

        if (UIScript.score >= 25 && UIScript.score < 50)
        {
            timeBetween = maxTimeBetween / 2f;
        }

        if (UIScript.score >= 50 && UIScript.score < 100)
        {
            timeBetween = maxTimeBetween / 2.5f;
        }

        if (UIScript.score >= 100 && UIScript.score < 200)
        {
            timeBetween = maxTimeBetween / 3f;
        }

        if (UIScript.score >= 200 && UIScript.score < 500)
        {
            timeBetween = maxTimeBetween / 5f;
        }

        if (UIScript.score >= 500 && UIScript.score < 1000)
        {
            timeBetween = maxTimeBetween / 10f;
        }

        if (UIScript.score >= 1000 && UIScript.score < 2000)
        {
            timeBetween = maxTimeBetween / 20f;
        }

        if (UIScript.score >= 2000)
        {
            timeBetween = maxTimeBetween / 50f;
        }

    }

    //Change location of spawn points after each spawn
    void ChangeLoc()
    {
        //if the spawn is at the initial location
        if (home)
        {
            //move to a random location
            randF = Random.Range(0f, 100f);
            //this section is the only difference from the SpawnLocScript
            //the top side spawners only want to move on the x axis while the other spawners move on both x and y
            if (randF > 50)
            {
                transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - 2f, transform.position.y);
            }

            //spawner is not at initial location, we track that here
            home = false;

        }
        //if the spawn is not at initial location
        else
        {
            //move back to initial location
            transform.position = homeLoc;

            //spawner is at initial location, we track that here
            home = true;
        }
    }
}
