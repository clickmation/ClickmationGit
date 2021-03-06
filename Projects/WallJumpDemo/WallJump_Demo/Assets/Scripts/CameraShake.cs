﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Camera mainCam;

    float shakeAmount = 0;
    float amtRate = 0.01f;
    float lengthRate = 0.01f;
    Vector3 oriCamPos;

    void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }

    public void Shake(float amt)
    {
        shakeAmount = amtRate * amt;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", 0.1f);
    }

    void BeginShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = mainCam.transform.position;
            oriCamPos = camPos;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }
    }

    void StopShake()
    {
        CancelInvoke("BeginShake");
        mainCam.transform.localPosition = oriCamPos;
    }
}
