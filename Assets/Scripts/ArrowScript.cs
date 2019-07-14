using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int life = 100;
    public float damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        speed = CharacterScript.shotPower;
        damage = CharacterScript.shotPower;
        rb.velocity = transform.right * speed;
        CharacterScript.shotPower = 0f;
    }

    void Update()
    {
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
            slime.TakeDmg(damage);
        }
    }
}
