using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerM : NetworkBehaviour
{
    private Vector3 touchPos;
    private Rigidbody rb;
    private Vector3 dir;
    private float ms = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = 0;
                dir = (touchPos - transform.position);
                rb.velocity = new Vector3(dir.x, dir.y,dir.z) * ms;

                if (touch.phase == TouchPhase.Ended)
                {
                    rb.velocity = Vector3.zero;
                }
            }
        }
       
        
    }
}
