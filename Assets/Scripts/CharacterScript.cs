using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private Rigidbody2D body;
    private bool canShoot = true;
    private bool isShooting = false;
    private Vector3 arrowTarPos;
    private int shotDelayCounter;

    public GameObject crosshair;
    public GameObject weapon;
    public GameObject arrow;
    public float speed;
    public float weaponDistance;
    public int health = 3;
    public int shotDelay = 40;

    // Start is called before the first frame update
    void Start()
    {
        //Assign the variable body to the rigid body component attached to this game object
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Logic Functions
        CharacterMovement();
        CrosshairMovement();
        WeaponMovement();
        if (Input.GetAxis("Fire1") == 1)
        {
            Shoot();
        }
    }

    //Move Character Based on player input
    private void CharacterMovement()
    {
        Vector2 pos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        body.MovePosition(new Vector2((transform.position.x + pos.x * speed * Time.deltaTime), transform.position.y + pos.y * speed * Time.deltaTime));
    }

    //Places the crosshair where the mouse is located on screen
    private void CrosshairMovement()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 1.0f;
        crosshair.transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    private void WeaponMovement()
    {

        Vector3 pos = Input.mousePosition;
        pos.z = (transform.position.z - Camera.main.transform.position.z);
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos = pos - transform.position;
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        if (angle < 0.0f) angle += 360.0f;
        weapon.transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);

        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * weaponDistance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * weaponDistance;
        weapon.transform.localPosition = new Vector3(transform.position.x + xPos, transform.position.y + yPos, 0.0f);
    }

    private void Shoot()
    {


        if (canShoot)
        {
            Debug.DrawLine(transform.position, crosshair.transform.position, Color.white, 0f);
            Instantiate(arrow, weapon.transform.position, weapon.transform.rotation);
            arrowTarPos = crosshair.transform.position;
            canShoot = false;
            isShooting = true;
        }

        if (isShooting)
        {
            arrow.transform.position = Vector2.MoveTowards(arrow.transform.position, arrowTarPos, 10.0f * Time.deltaTime);

            if (arrow.transform.position == arrowTarPos)
            {
                isShooting = false;
            }
        }

        if (!canShoot)
        {
            if (shotDelayCounter < shotDelay)
            {
                shotDelayCounter++;
            }
            else
            {

                canShoot = true;
                shotDelayCounter = 0;
            }
        }
    }
}
