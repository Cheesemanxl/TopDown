using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeBetween = 10f;
    public float maxTimeBetween = 10f;
    public float targetSpawn = 90f;
    private float randF = 0f;

    private bool home = true;
    private Vector2 homeLoc;

    public GameObject slime;
    private CharacterScript CharacterScript;




    void Start()
    {
        timeBetween = Random.Range(0, 500);
        CharacterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
        homeLoc = transform.position;
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
                    ChangeLoc();
                }

                timeBetween = maxTimeBetween;
            }
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
