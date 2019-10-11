using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCollider : MonoBehaviour
{
    public Movement mov;
    [SerializeField] private Camera camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(camera.ScreenToWorldPoint(Input.mousePosition));
            Vector2 mPos = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(mPos, 0.1f * Vector2.one, 0.1f, 1 <<  LayerMask.NameToLayer("TouchCollider"));
            if (hit)
            {
                mov.Attack();
            }
        }
        //else if (Input.GetMouseButtonUp(0))
        //{

        //}
        //else if (Input.GetMouseButton(0))
        //{

        //}
    }
}
