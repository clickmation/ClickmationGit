using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpCollider : MonoBehaviour
{
    public Movement mov;
    [SerializeField] private Camera camera;
    [SerializeField] private Button.ButtonClickedEvent jumpUp;
    [SerializeField] private Button.ButtonClickedEvent jumpDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(camera.ScreenToWorldPoint(Input.mousePosition));
            Vector2 mPos = new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(mPos, 0.1f * Vector2.one, 0.1f, 1 << LayerMask.NameToLayer("TouchCollider"));
            if (hit)
            {
                mov.jump = true;
                jumpDown.Invoke();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
                mov.jump = false;
                jumpUp.Invoke();
        }
        //else if (Input.GetMouseButton(0))
        //{

        //}
    }
}
