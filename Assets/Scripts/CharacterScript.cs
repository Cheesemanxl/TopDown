using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    private Rigidbody2D body;
    private Image[] hearts;
    public GameObject powerBar;
    public Transform shootPoint;
    public GameObject crosshair;
    public Transform weapon;
    public GameObject arrowPrefab;
    public Sprite full;
    public Sprite empty;

    public float speed = 12f;
    public static float shotPower = 0f;

    public int health = 5;
    public int currentHearts = 3;
    public int immunityFrames = 0;
    private bool immune = false;

    // Start is called before the first frame update
    void Start()
    {
        //Assign the variable body to the rigid body component attached to this game object
        body = GetComponent<Rigidbody2D>();
        //Hide cursor on start up
        Cursor.visible = false;
        hearts = new Image[5];
        hearts[0] = GameObject.FindGameObjectWithTag("Heart1").GetComponent<Image>();
        hearts[1] = GameObject.FindGameObjectWithTag("Heart2").GetComponent<Image>();
        hearts[2] = GameObject.FindGameObjectWithTag("Heart3").GetComponent<Image>();
        hearts[3] = GameObject.FindGameObjectWithTag("Heart4").GetComponent<Image>();
        hearts[4] = GameObject.FindGameObjectWithTag("Heart5").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Logic Functions
        CharacterMovement();
        CrosshairMovement();
        WeaponMovement();

        //Health UI Handler
        HeartDisplay();
        CheckImmunity();

        //Shoot Function
        if (Input.GetButton("Fire1"))
        {
            if (shotPower < 20f)
            {
                shotPower = shotPower + 0.1f;
                powerBar.transform.localScale = new Vector3((shotPower/20f), 1f, 1f);
            }
            
        }

        if (Input.GetButtonUp("Fire1"))
        {
            powerBar.transform.localScale = new Vector3(0f, 1f, 1f);
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
        Vector2 direction = new Vector2(pos.x - weapon.position.x, pos.y - weapon.position.y);

        weapon.right = direction;
    }

    private void Shoot()
    {
        Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
    }

    public void TakeDmg()
    {
            if (health <= 0)
            {
                //Death();
                //Debug.Log("You Died!");
            }

        if (!immune)
        {
            health = health - 1;
            immune = true;
            immunityFrames = 70;
        }
    }

    private void CheckImmunity()
    {
        immunityFrames--;
        if (immunityFrames < 0)
        {
            immune = false;
        }
    }

    private void HeartDisplay()
    {
        if (health > currentHearts)
        {
            health = currentHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = full;
            }
            else
            {
                hearts[i].sprite = empty;
            }

            if (i < currentHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
