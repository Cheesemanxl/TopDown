using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private Rigidbody2D body;
    public GameObject crosshair;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
        CrosshairMovement();
        Debug.Log(Input.GetAxis("Fire1"));

        if (Input.GetAxis("Fire1") == 1)
        {
            Debug.DrawLine(transform.position,crosshair.transform.position, Color.white, 0f);
        }
        
    }

    private void CharacterMovement()
    {
        Vector2 pos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        body.MovePosition(new Vector2((transform.position.x + pos.x * speed * Time.deltaTime), transform.position.y + pos.y * speed * Time.deltaTime));
    }

    private void CrosshairMovement()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 1.0f;
        crosshair.transform.position = Camera.main.ScreenToWorldPoint(pos);
    }
}
