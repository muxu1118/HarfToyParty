using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBomb : MonoBehaviour
{

    [SerializeField]
    private float touchRangeX;
    [SerializeField]
    private float touchRangeY;

    [SerializeField]
    private Player MyPlayer;

    private Vector2 movePosition;

    protected Vector2 TouchPosition = Vector2.zero;
    protected Vector2 StartPosition = Vector2.zero;

    // タップしたオブジェクト
    protected GameObject tapObject;
    private Canvas canvas;
    protected int[] nowTouching = new int[5];

    GameObject Player;

    private void Update()
    {
        GetClick();
    }

    // Swipe 
    private void GetClick()
    {
        // スマホタップの取得 touchCountが0以上でタップ判定
        if (Input.GetMouseButtonDown(0))
        {
            Player = GameObject.Find("Player1");
            Vector3 v3 = new Vector3();
            v3 = GameObject.Find("BombButton").transform.position;
            Vector2 vec2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if ((vec2.x >= v3.x - 0.5f) && (vec2.x <= v3.x + 0.5f) && (vec2.y >= v3.y - 0.5f) && (vec2.y <= v3.y + 0.5f))
            {
                if (Player.gameObject.name == "Player1")
                {
                    Map.instance.BombAria(Player.GetComponent<Player>().rot, MapKind.Player1);
                }
                else if (Player.gameObject.name == "Player2")
                {
                    Map.instance.BombAria(Player.GetComponent<Player>().rot, MapKind.Player2);
                }
            }

        }

    }
}
