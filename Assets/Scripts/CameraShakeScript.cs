using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeScript : MonoBehaviour
{
    private Vector3 homeLoc;
    public float length;
    public float magnitude;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        //Set initial camera location
        homeLoc = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //move camera to a random location within a certain sphere relative to the initial location
        if (length > 0)
        {
            transform.localPosition = homeLoc + Random.insideUnitSphere * magnitude;

            length -= Time.deltaTime * speed;
        }
        //set length to be exactly 0 and return to the initial camera location
        else
        {
            length = 0f;
            transform.localPosition = homeLoc;
        }
    }

    //Collect values to produce shake
    public void Shake(float l, float m, float s)
    {
        length = l;
        magnitude = m;
        speed = s;
    }
}
