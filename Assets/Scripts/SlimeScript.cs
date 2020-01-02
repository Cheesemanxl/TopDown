using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    private int dmgDelayCounter;
    public float maxDmgDistance;
    private Transform target;
    private CharacterScript CharacterScript;

    private CameraShakeScript cam;
    public GameObject healthBar;
    public UIScript ui;
    public float speed = .05f;
    public int dmgDelay;
    public float health = 10f;
    private float maxHealth = 0f;
    private bool onScreen = false;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CharacterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeScript>();
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        healthBar.transform.localScale = new Vector3((health / maxHealth), 1f, 1f);
    }

    //if the slime is not VERY close to player move towards player
    void Movement()
    {
        if (Vector2.Distance(transform.position, target.position) > maxDmgDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    //damage taking logic
    public void TakeDmg(float dmg)
    {
        //Determine whether the slime can be seen
        if (onScreen) { 

            //hurt slime if so
            health = health - dmg;

            //call the death function if slime life is less than or equal to 0
            if (health <= 0)
            {
                Death();
            }
        }
    }

    //add one to the score and destroy this slime object
    void Death()
    {
        ui.UpScore();
        Destroy(gameObject);
    }

    //called when overlapping a trigger collider
    void OnTriggerEnter2D(Collider2D col)
    {
        //if the collider is attached to a player deal damage to the player
        CharacterScript player = col.GetComponent<CharacterScript>();
        if (player != null)
        {
            player.TakeDmg();
        }
    }
    
    //Called when the object is within the view of the camera object
    void OnBecameVisible()
    {
        onScreen = true;
    }
}

