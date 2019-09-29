using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollider : MonoBehaviour
{
    [SerializeField] Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hoo...");
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        movement.jump = true;
        Debug.Log("Clicked");
    }

    private void OnMouseUp()
    {
        movement.jump = false;
    }

    void OnMouseEnter()
    {
        Debug.Log("WTF What's the problem");
    }
}
