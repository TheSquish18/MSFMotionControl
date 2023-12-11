using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public AudioSource forest;
    public AudioSource sky;
    public CameraScript cameraScript;
    private bool notYet = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (cameraScript.highestY > 12)
        {
            if (notYet == true)
            {
                notYet = false;
                sky.Play();
            }
            else
            {
                forest.volume -= 0.04f;
                sky.volume += 0.04f;
                if (forest.volume <= 0 && sky.volume >= 1)
                {
                    forest.Stop();
                    sky.volume = 1;
                }
            }
        }
    }
}
