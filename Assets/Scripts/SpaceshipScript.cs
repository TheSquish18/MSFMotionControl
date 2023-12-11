using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipScript : MonoBehaviour
{
    public int forcePower = 1;

    void Start()
    {
        StartCoroutine(Float());
    }

    void Update()
    {
        GetComponent<ConstantForce2D>().force = new Vector2(0, forcePower);
        GetComponent<ConstantForce2D>().relativeForce = new Vector2(0, forcePower);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bunny")
        {
            collision.gameObject.GetComponent<GyroScript>().rocketTime = true;
            Destroy(gameObject);
        }
    }

    private IEnumerator Float()
    {
        yield return new WaitForSeconds(1f);
        forcePower *= -1;
        yield return new WaitForSeconds(3f);
        forcePower *= -1;
        StartCoroutine(Float());
    }
}
