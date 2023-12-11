using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidScript : MonoBehaviour
{

    private float startGame = 11;
    private float endGame = 11;
    private int forcePower = 1;
    public AudioSource hitNoise;


    void Start()
    {
        StartCoroutine(StopZooming());
        if (transform.position.x > 0)
        {
            endGame *= -1;
            forcePower *= -1;
        }
        else
        {
            startGame *= -1;
        }
        GetComponent<ConstantForce2D>().force = new Vector2(forcePower, GetComponent<ConstantForce2D>().force.y);
        GetComponent<ConstantForce2D>().relativeForce = new Vector2(forcePower, GetComponent<ConstantForce2D>().relativeForce.y);
    }

    void Update()
    {

        if (transform.position.x*forcePower >= endGame*forcePower)
        {
            transform.position = new Vector2(startGame, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bunny")
        {
            hitNoise.Play();
            StartCoroutine(Reset());
        }
    }

    IEnumerator StopZooming()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<ConstantForce2D>().enabled = false;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
}
