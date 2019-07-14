using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    private int dmgDelayCounter;
    public float maxDmgDistance;
    private Transform target;
    private CharacterScript CharacterScript;

    public GameObject healthBar;
    public UIScript ui;
    public float speed = .05f;
    public int dmgDelay;
    public float health = 10f;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CharacterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        healthBar.transform.localScale = new Vector3((health / 20f), 1f, 1f);
    }

    void Movement()
    {
        if (Vector2.Distance(transform.position, target.position) > maxDmgDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void TakeDmg (float dmg)
    {
        health = health - dmg;

        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        ui.UpScore();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        CharacterScript player = col.GetComponent<CharacterScript>();
        if (player != null)
        {
            player.TakeDmg();
        }
    }
}

