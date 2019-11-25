using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BombButton : Tap
{
    [SerializeField]
    private float touchRangeX;
    [SerializeField]
    private float touchRangeY;


    private Vector2 movePosition;
    public Player MyPlayer { get; set; }
    GameObject BombAria;
    private void Awake()
    {

    }
    private void Start()
    {
        //Debug.Log(isServer);
        //MyPlayer = GameObject.Find("Player1").GetComponent<Player>();
        StartCoroutine(SetButton());
    }
    IEnumerator SetButton()
    {
        yield return new WaitForSeconds(Time.deltaTime*4);

        Debug.Log(GameObject.Find("Player1"));
        Debug.Log(GameObject.Find("Player2"));
        if (GameObject.Find("Player2") == null)
        {
            MyPlayer = GameObject.Find("Player1").GetComponent<Player>();
        }
        else
        {
            MyPlayer = GameObject.Find("Player2").GetComponent<Player>();
        }
        yield return new WaitForSeconds(0.5f);
        if (MyPlayer == null)
        {
            yield return new WaitForSeconds(1.5f);
            MyPlayer = GameObject.Find("Player2").GetComponent<Player>();
        }
        BombAria = (MyPlayer.name == "Player1")? GameObject.Find("BombAria1"): GameObject.Find("BombAria2");
        

    }
    

    private void Update()
    {
        GetTap();
    }

    // Swipe 
    public override void GetTap()
    {
        base.GetTap();
        if (Input.touchCount <= 0) return;

        Touch touch = Input.GetTouch(nowTouching[1]);
        if (touch.phase == TouchPhase.Began)
        {
            Vector2 vec2 = Camera.main.ScreenToWorldPoint(touch.position);
            if (vec2.x <= transform.position.x + touchRangeX && vec2.x >= transform.position.x - touchRangeX && vec2.y <= transform.position.y + touchRangeY && vec2.y >= transform.position.y - touchRangeY)
            {
                Debug.Log("今はギミックボタンを押しています");

                MapKind P = (MyPlayer.name == "Player1") ? MapKind.Player1 : MapKind.Player2;
                MapKind A = (P == MapKind.Player1) ? MapKind.BombAria1 : MapKind.BombAria2;
                
                if (IsBomb)
                {
                    IsBomb = false;
                    // 爆弾を置く
                    Map.instance.BombAria(MyPlayer.rot, P);
                }
                else
                {
                    IsBomb = true;
                    // 爆弾範囲をだす
                    MapKind AriaKind = (MyPlayer.name == "Player1") ? MapKind.BombAria1 : MapKind.BombAria2;
                    Map.instance.AriaSet(P, AriaKind);
                }
            }
        }

       
    }
}
