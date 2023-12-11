using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private AudioSource landingNoise;

    void Start()
    {
        landingNoise = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        landingNoise.Play();
    }
}
