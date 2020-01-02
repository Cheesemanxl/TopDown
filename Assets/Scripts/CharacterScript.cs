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
    private Canvas deathPanel;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private CameraShakeScript cam;

    public float speed = 12f;
    public static float shotPower = 0f;

    private int frameCount = 144;
    public int health = 5;
    public int currentHearts = 3;
    private int immunityFrames = 0;
    private bool immune = false;
    public bool dead = false;
    private bool red = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        deathPanel = GameObject.FindGameObjectWithTag("Death Panel").GetComponent<Canvas>();
        deathPanel.enabled = false;

        //Assign the variable body to the rigid body component attached to this game object
        body = GetComponent<Rigidbody2D>();

        //Hide cursor on start up
        Cursor.visible = false;

        //Create an array of images and assign the heart ui objects to them
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
        //if player is alive
        if (!dead) {

            //Movement Logic Functions
            CharacterMovement();
            CrosshairMovement();
            WeaponMovement();

            //immunity animation function
            FlashRed();

            //when the left mouse is held down
            if (Input.GetButton("Fire1"))
            {
                //if shot power is lower than or equal to 19.8
                if (shotPower <= 19.8f)
                {
                    //add 0.2 to shot power
                    shotPower = shotPower + 0.2f;
                    //set power bar length to a percentage of the shot power
                    powerBar.transform.localScale = new Vector3((shotPower / 20f), 1f, 1f);
                }

            }

            //when left mouse button is released
            if (Input.GetButtonUp("Fire1"))
            {
                //reset power bar UI object
                powerBar.transform.localScale = new Vector3(0f, 1f, 1f);

                //shoot an arrow
                Shoot();
            }
        }

        //Health UI Handler
        HeartDisplay();
        CheckImmunity();
    }

    //Move Character Based on player input
    private void CharacterMovement()
    {
        //create a new vector 2 based on the horizontal and vertical axes in Unity (determined by WASD and arrow keys)
        Vector2 pos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //move character closer to the pos vector2, based on base speed variable
        body.MovePosition(new Vector2((transform.position.x + pos.x * speed * Time.deltaTime), transform.position.y + pos.y * speed * Time.deltaTime));

        //determine if character sprite should be facing towards or away from camera
        animator.SetFloat("Vertical", pos.y);
        
        
        //set pos coords to be positive if they are not positive
        pos.x = Mathf.Abs(pos.x);
        pos.y = Mathf.Abs(pos.y);

        //determine animator speed
        if (pos.x > 0 | pos.y > 0)
        {
            animator.SetFloat("Speed", 1f);
        } else
        {
            animator.SetFloat("Speed", 0f);
        }
        
    }

    //Places the crosshair where the mouse is located on screen
    private void CrosshairMovement()
    {
        //get the position of the mouse cursor
        Vector3 pos = Input.mousePosition;

        //set the z coord to 1
        pos.z = 1.0f;

        //move the crosshair sprite to the mouse position
        crosshair.transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    //place the bow to a location around the player based on mouse position
    private void WeaponMovement()
    {
        //get the position of the mouse cursor
        Vector3 pos = Input.mousePosition;

        //set this position z based on camera position z
        pos.z = (transform.position.z - Camera.main.transform.position.z);

        //set position to world point based on camera
        pos = Camera.main.ScreenToWorldPoint(pos);

        //determine direction bow should be pointing
        Vector2 direction = new Vector2(pos.x - weapon.position.x, pos.y - weapon.position.y);

        //set bow position
        weapon.right = direction;
    }

    //create an arrow prefab at shoot point location and rotation
    private void Shoot()
    {
        Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
    }

    //deals damage to the player, determines death and starts immunity
    public void TakeDmg()
    {
        //kill player if health is 0 or lower
        if (health <= 0)
        {
            Death();
        }

        //if the player is not immune
        if (!immune)
        {
            //make sprite red
            spriteRenderer.material.color = new Color32(255, 0, 0, 222);

            //shake camera with these params
            cam.Shake(0.001f, 0.1f, 0.1f);

            //lower health
            health = health - 1;
            //start immunity and set immunity frames
            immune = true;
            immunityFrames = 144;
            red = true;
            
        }
    }

    //flashes red when hit
    private void FlashRed()
    {
        if(red)
        {

            //change the color of the sprite every few frames
            frameCount--;
            if (frameCount == 126)
            {
                spriteRenderer.material.color = new Color32(255, 255, 255, 222);
            }

            if (frameCount == 108)
            {
                spriteRenderer.material.color = new Color32(255, 0, 0, 222);
            }

            if (frameCount == 90)
            {
                spriteRenderer.material.color = new Color32(255, 255, 255, 222);
            }

            if (frameCount == 72)
            {
                spriteRenderer.material.color = new Color32(255, 0, 0, 222);
            }

            if (frameCount ==54)
            {
                spriteRenderer.material.color = new Color32(255, 255, 255, 222);
            }

            if (frameCount == 36)
            {
                spriteRenderer.material.color = new Color32(255, 0, 0, 222);
            }

            if (frameCount == 18)
            {
                spriteRenderer.material.color = new Color32(255, 255, 255, 255);
            }

            //stop flashing and reset frame count
            if (frameCount == 0)
            {
                frameCount = 144;
                red = false;
            }
        }
    }

    //make character immune for a certain amount of frames
    private void CheckImmunity()
    {
        immunityFrames--;
        if (immunityFrames < 0)
        {
            immune = false;
        }
    }


    //change the hearts on the ui display based on character life
    private void HeartDisplay()
    {
        //make sure hearts and life are equal
        if (health > currentHearts)
        {
            health = currentHearts;
        }

        //change hearts to full or empty based on player health 
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

    //stop all movement and show the death screen
    private void Death()
    {
        dead = true;
        Cursor.visible = true;
        deathPanel.enabled = true;
        animator.SetFloat("Vertical", 0f);
        animator.SetFloat("Speed", 0f);
    }
}
