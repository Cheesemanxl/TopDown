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
        homeLoc = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (length > 0)
        {
            transform.localPosition = homeLoc + Random.insideUnitSphere * magnitude;

            length -= Time.deltaTime * speed;
        }
        else
        {
            length = 0f;
            transform.localPosition = homeLoc;
        }
    }

    public void Shake(float l, float m, float s)
    {
        length = l;
        magnitude = m;
        speed = s;
    }
}
