using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeightScript : MonoBehaviour
{
    public CameraScript cameraScript;
    private TMP_Text counter;

    void Start()
    {
        counter = GetComponent<TMP_Text>();
    }

    void Update()
    {
        counter.text = (Mathf.RoundToInt(cameraScript.highestY)*10).ToString();
    }
}
