using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class GyroScript : MonoBehaviour
{
    Gyroscope m_Gyro;
    private bool isGrounded;
    private float yprevious;
    public Animator animator;

    public float forceTest;
    public bool rocketTime = false;
    private bool flying = false;
    public AudioSource rocketNoise;
    public AudioSource jumpNoise;
    public AudioSource lossNoise;

    //private bool doJumpNoise = true;

    void Start()
    {
        //Set up and enable the gyroscope (check your device has one)
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
        yprevious = transform.position.y;
        forceTest = 11;

        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    //This is a legacy function, check out the UI section for other ways to create your UI
    void OnGUI()
    {
        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        //GUI.Label(new Rect(500, 300, 200, 40), "Gyro rotation rate " + m_Gyro.rotationRate);
        //GUI.Label(new Rect(500, 350, 200, 40), "Gyro attitude" + m_Gyro.attitude);
        //GUI.Label(new Rect(500, 400, 200, 40), "Gyro enabled : " + m_Gyro.enabled);
    }

    void FixedUpdate()
    {
        if (transform.position.y == yprevious)
        {
            isGrounded = true;
        }
        else { isGrounded = false; }
        yprevious = transform.position.y;


        ///whats going on
        //Vector3 gyroEuler = m_Gyro.attitude.eulerAngles;
        //transform.eulerAngles = new Vector3(-1.0f * gyroEuler.x, -1.0f * gyroEuler.y, gyroEuler.z);
        //Vector3 upVec = transform.InverseTransformDirection(-1.0f * Vector3.forward);
        ///up in this bitch


        //tilt
        if (m_Gyro.attitude.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            GetComponent<ConstantForce2D>().force = new Vector2(forceTest, GetComponent<ConstantForce2D>().force.y);
            GetComponent<ConstantForce2D>().relativeForce = new Vector2(forceTest, GetComponent<ConstantForce2D>().relativeForce.y);

        }
        else if (m_Gyro.attitude.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            GetComponent<ConstantForce2D>().force = new Vector2(-forceTest, GetComponent<ConstantForce2D>().force.y);
            GetComponent<ConstantForce2D>().relativeForce = new Vector2(-forceTest, GetComponent<ConstantForce2D>().relativeForce.y);
        }
        else
        {
            GetComponent<ConstantForce2D>().force = new Vector2(0, GetComponent<ConstantForce2D>().force.y);
            GetComponent<ConstantForce2D>().relativeForce = new Vector2(0, GetComponent<ConstantForce2D>().relativeForce.y);
        }

        //jumping??
        if ((Input.acceleration.z > 1 || Input.acceleration.z < -1) && isGrounded == true && flying == false)
        {
            GetComponent<ConstantForce2D>().force = new Vector2(GetComponent<ConstantForce2D>().force.x, forceTest*2);
            GetComponent<ConstantForce2D>().relativeForce = new Vector2(GetComponent<ConstantForce2D>().relativeForce.x, forceTest*2);
            StartCoroutine(StopJump());
            jumpNoise.Play();

        }

        IEnumerator StopJump()
        {
            yield return new WaitForSeconds(0.4f);
            GetComponent<ConstantForce2D>().force = new Vector2(GetComponent<ConstantForce2D>().force.x*1.2f, forceTest*-1.5f);
            GetComponent<ConstantForce2D>().relativeForce = new Vector2(GetComponent<ConstantForce2D>().relativeForce.x*1.2f, forceTest*-1.5f);
            //doJumpNoise = false;
        }


        //rocket
        if (rocketTime == true)
        {
            GetComponent<ConstantForce2D>().force = new Vector2(GetComponent<ConstantForce2D>().force.x, forceTest);
            GetComponent<ConstantForce2D>().relativeForce = new Vector2(GetComponent<ConstantForce2D>().relativeForce.x, forceTest);
            animator.SetBool("rocketTime", true);
            rocketNoise.Play();
            rocketTime = false;
            flying = true;
        }
    }
}

