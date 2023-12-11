using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    private GameObject bunny;
    private Vector2 ogPosition;
    public float highestY;
    public AudioSource winNoise;
    public AudioSource fallNoise;

    void Start()
    {
        bunny = GameObject.Find("Bunny");
        ogPosition = bunny.transform.position;
        highestY = bunny.transform.position.y;
    }

    void Update()
    {
        if (bunny.transform.position.y < 0)
        {
            highestY = 0;
        }
        else if (bunny.transform.position.y > highestY)
        {
            highestY = bunny.transform.position.y;
        }

        if (highestY > 100)
        {
            highestY = 100;
        }
        
        transform.position = new Vector3(ogPosition.x, highestY, -100);

        if (bunny.transform.position.y <= highestY - 6)
        {
            fallNoise.Play();
            StartCoroutine(Reset());
        }
        if (bunny.transform.position.y >= 150)
        {
            winNoise.Play();
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
       
}
