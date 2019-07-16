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
        speed = CharacterScript.shotPower;
        powerPercent = (CharacterScript.shotPower / 20f);
        damage = CharacterScript.shotPower;
        rb.velocity = transform.right * speed;
        CharacterScript.shotPower = 0f;
        life = maxLife;

        if ((maxLife % 2) == 1)
        {
            maxLife--;
        }
    }

    void Update()
    {
        SlowVelocity();

        life--;

        if (life < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        SlimeScript slime = col.GetComponent<SlimeScript>();
        if (slime != null)
        {
            if (rb.velocity != new Vector2(0f, 0f)) {
                slime.TakeDmg(damage);
            }
        }
    }

    void SlowVelocity()
    {
        if (life < (maxLife - ((maxLife / 2) * powerPercent)))
        {
            rb.velocity = new Vector2(0f, 0f);
        }

    }
}
