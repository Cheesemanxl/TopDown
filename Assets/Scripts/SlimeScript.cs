using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public float speed;
    public int dmgDelay;
    private int dmgDelayCounter;
    public float maxDmgDistance;
    private Transform target;
    private CharacterScript CharacterScript;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CharacterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Vector2.Distance(transform.position, target.position) > maxDmgDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            DoDmg();
        }
    }

    void DoDmg()
    {
        if (dmgDelayCounter <= dmgDelay)
        {
            dmgDelayCounter++;
        }
        else
        {
            CharacterScript.health--;
            Debug.Log(CharacterScript.health);
            dmgDelayCounter = 0;
        }
    }
}

