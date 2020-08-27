using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image health;
    public Text username;

    void FixedUpdate()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
        }
    }

    public void SetName(string _username)
    {
        username.text = _username;
    }

    public void UpdateHealth(float _healthPercent)
    {
        health.transform.localScale = new Vector3(_healthPercent, 1, 1);
    }
}
