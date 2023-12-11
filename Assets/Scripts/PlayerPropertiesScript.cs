using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropertiesScript : MonoBehaviour
{
    private float speedHorizontal = 0;
    private float speedVertical = 0;
    private bool stage1 = true;
    private bool stage2 = false;
    //private bool isGrounded = true;
    private Vector2 bunPosition;
    private float xprevious;

    void Start()
    {
        bunPosition = new Vector2(transform.position.x, transform.position.y);
        xprevious = transform.position.x;

        if (stage1 == true)
        {
            speedHorizontal = 0.00001f;
            speedVertical = 0;
        }
        if (stage2 == true)
        {
            speedHorizontal = 0;
            speedVertical = 0.00001f;
        }
    }

    void Update()
    {
        speedHorizontal *= 1.1f;
        speedVertical *= 1.1f;
        speedHorizontal = Mathf.Clamp(speedHorizontal, 0, .001f);
        speedVertical = Mathf.Clamp(speedHorizontal, 0, .001f);
        bunPosition.x += speedHorizontal;
        //bunPosition.y += speedVertical;
        transform.position = bunPosition;

        if (transform.position.x > xprevious)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        xprevious = transform.position.x;
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speedHorizontal *= -1;
    }
}
