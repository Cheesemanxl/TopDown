using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private int life;
    private int maxLife = 560;
    public float damage = 1;
    private float powerPercent;

    // Start is called before the first frame update
    void Start()
    {
        //Set Vars based on values from the character script
        speed = CharacterScript.shotPower;
        powerPercent = (CharacterScript.shotPower / 20f);
        damage = CharacterScript.shotPower;
        rb.velocity = transform.right * speed;
        CharacterScript.shotPower = 0f;
        life = maxLife;

        //Make sure maxLife is not odd and make it even if so
        if ((maxLife % 2) == 1)
        {
            maxLife--;
        }
    }

    //called every frame
    void Update()
    {
        //slow down arrow
        SlowVelocity();

        //decrement life
        life--;

        //destroy arrow if life is less than 0
        if (life < 0)
        {
            Destroy(gameObject);
        }
    }


    //Calls when a trigger collider is overlapped with this collider
    void OnTriggerEnter2D(Collider2D col)
    {
        //assign a slime var to the slime script component of the overlapped object
        SlimeScript slime = col.GetComponent<SlimeScript>();

        //if the overlapped object has a slime script..
        if (slime != null)
        {
            //if arrow is still moving..
            if (rb.velocity != new Vector2(0f, 0f)) {

                //deal damage to the specific slime that was overlapped
                slime.TakeDmg(damage);
            }
        }
    }

    //Slows the arrow down
    void SlowVelocity()
    {
        if (life < (maxLife - ((maxLife / 2) * powerPercent)))
        {
            rb.velocity = new Vector2(0f, 0f);
        }

    }
}
