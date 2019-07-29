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
        if (!dead) {
            CharacterMovement();
            CrosshairMovement();
            WeaponMovement();
            FlashRed();

            //Shoot Function
            if (Input.GetButton("Fire1"))
            {
                if (shotPower <= 19.8f)
                {
                    shotPower = shotPower + 0.2f;
                    powerBar.transform.localScale = new Vector3((shotPower / 20f), 1f, 1f);
                }

            }

            if (Input.GetButtonUp("Fire1"))
            {
                powerBar.transform.localScale = new Vector3(0f, 1f, 1f);
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
        Vector2 pos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        body.MovePosition(new Vector2((transform.position.x + pos.x * speed * Time.deltaTime), transform.position.y + pos.y * speed * Time.deltaTime));

        animator.SetFloat("Vertical", pos.y);
        pos.x = Mathf.Abs(pos.x);
        pos.y = Mathf.Abs(pos.y);

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
                Death();
            }

        if (!immune)
        {
            spriteRenderer.material.color = new Color32(255, 0, 0, 222);
            cam.Shake(0.001f, 0.1f, 0.1f);
            health = health - 1;
            immune = true;
            immunityFrames = 144;
            red = true;
            
        }
    }
    private void FlashRed()
    {
        if(red)
        {

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

            if (frameCount == 0)
            {
                frameCount = 144;
                red = false;
            }
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

    private void Death()
    {
        dead = true;
        Cursor.visible = true;
        deathPanel.enabled = true;
        animator.SetFloat("Vertical", 0f);
        animator.SetFloat("Speed", 0f);
    }
}
