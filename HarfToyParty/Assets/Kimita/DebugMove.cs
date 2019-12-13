using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DebugMove : NetworkBehaviour
{
    [SerializeField]
    private float touchRangeX;
    [SerializeField]
    private float touchRangeY;

    [SerializeField]
    private Player MyPlayer;

    private Vector2 movePosition;
    private bool swiChe;

    protected Vector2 TouchPosition = Vector2.zero;
    protected Vector2 StartPosition = Vector2.zero;

    // タップしたオブジェクト
    protected GameObject tapObject;
    private Canvas canvas;
    protected int nowTouching;

    private Vector3 ButtonPos = new Vector3();

    private void Start()
    {
        ButtonPos = GameObject.Find("MoveButton").transform.position;
        MyPlayer.SetActionCallback(PlayerDoNotMove);
        //StartCoroutine("FindPly");
    }

    //IEnumerator FindPly()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    MyPlayer = transform.root.GetChild(0).gameObject.GetComponent<Player>();

    //}
    private void Update()
    {
        
            GetClick();
        
        Swipe();
    }

    // Swipe 
    private void GetClick()
    {
        if (isLocalPlayer) {
            if (Input.GetMouseButtonDown(0))
            {
                TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                StartPosition = TouchPosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StartPosition = Vector2.zero;
                swiChe = false;
            }
            else if (Input.GetMouseButton(0)) {
                TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (StartPosition.x <= ButtonPos.x + touchRangeX && StartPosition.x >= ButtonPos.x - touchRangeX && StartPosition.y <= ButtonPos.y + touchRangeY && StartPosition.y >= ButtonPos.y - touchRangeY) {
                    swiChe = true;
                }
            }

        }
    }

    private void Swipe()
    {
        if (swiChe == false)
            return;
        if (TouchPosition.y > StartPosition.y + 0.1f && Mathf.Abs(TouchPosition.y) - StartPosition.y >= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            MyPlayer.Move(0);
        }
        else if (TouchPosition.y < StartPosition.y - 0.1f && Mathf.Abs(TouchPosition.y) - StartPosition.y >= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            MyPlayer.Move(1);
        }
        else if (TouchPosition.x > StartPosition.x + 0.1f)
        {
            MyPlayer.Move(2);
        }
        else if (TouchPosition.x < StartPosition.x - 0.1f)
        {
            MyPlayer.Move(3);
        }

    }

    /// <summary>
    /// プレイヤーが移動できなくなったら呼び出される
    /// </summary>
    private void PlayerDoNotMove()
    {
        swiChe = false;
        TouchPosition = StartPosition;
    }
}
