using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeBetween = 10f;
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
        CharacterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
        homeLoc = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CharacterScript.dead) {
            SpawnTimingLogic();
        }
    }

    void SpawnTimingLogic()
    {
        timeBetween--;

        if (timeBetween < 0)
        {
            randF = Random.Range(0f, 100f);

            if (randF > targetSpawn)
            {

                Instantiate(slime, transform.position, transform.rotation);
                ChangeLoc();
            }

            DecideTimeBetween();
        }
    }

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

    void ChangeLoc()
    {
        if (home)
        {
            randF = Random.Range(0f, 100f);
            if (randF > 50) {
                randF = Random.Range(0f, 100f);
                if (randF > 50)
                {
                    transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x - 2f, transform.position.y);
                }
            }
            else
            {
                randF = Random.Range(0f, 100f);
                if (randF > 50)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + 2f);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y - 2f);
                }
            }

            home = false;

        }

        else

        {
            transform.position = homeLoc;
            home = true;
        }
    }
}
