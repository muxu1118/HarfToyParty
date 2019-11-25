using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 */ 

public class MoveButton : Tap
{
    [SerializeField]
    private float touchRangeX;
    [SerializeField]
    private float touchRangeY;
    

    private Vector2 movePosition;
    private bool swiChe;
    public Player MyPlayer { get; set; }
    Tap tap;
    private void Start()
    {
      
    }
    private void Update()
    {
        GetTap();
        Swipe();
    }

    // Swipe 
    public override void GetTap()
    {
        base.GetTap();
        if (Input.touchCount <= 0) return;
        
        Touch touch = tc[nowTouching[0]];
        if (touch.phase == TouchPhase.Ended)
        {
            StartPosition = Vector2.zero;
            swiChe = false;
        }


        if (StartPosition.x <= transform.position.x+touchRangeX&&StartPosition.x >= transform.position.x - touchRangeX&& StartPosition.y <= transform.position.y + touchRangeY && StartPosition.y >= transform.position.y  -touchRangeY)
        {
            swiChe = true;
        }

    }
    private void Swipe()
    {
        if (!swiChe) return;
        Touch touch = tc[nowTouching[0]];
        TouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        if (TouchPosition.x > StartPosition.x + 0.1f&& Mathf.Abs(TouchPosition.y) - StartPosition.y <= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            MyPlayer.Move(2);
        }
        else if (TouchPosition.x < StartPosition.x - 0.1f && Mathf.Abs(TouchPosition.y) - StartPosition.y <= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            MyPlayer.Move(3);
        }
        else if (TouchPosition.y > StartPosition.y + 0.1f&& Mathf.Abs(TouchPosition.y) - StartPosition.y >= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            MyPlayer.Move(0);
        }
        else if (TouchPosition.y < StartPosition.y - 0.1f && Mathf.Abs(TouchPosition.y) - StartPosition.y >= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            MyPlayer.Move(1);
        }

    }


}
