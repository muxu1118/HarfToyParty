using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Player MyPlayer;
    private bool isbomb;

    float Recast = 7;
    [SerializeField]
    float Recastlimit = 7;
    public bool Isbomb { get => isbomb;}
    float crash;
    float crashLimit = 1;
    [SerializeField]
    Sprite psp;//PlayerのSprite
    bool ispulling = false;//壁を引いてるか
    int pullWallN = 0;//引いてる壁の値

    // Start is called before the first frame update
    void Start()
    {
        float x = Input.GetAxisRaw("Horizontal");
        isbomb = false;
        crash = 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        InputMove();
        ActionInput();
        WallGrab();
        Recast += Time.deltaTime;
        crash += Time.deltaTime;

    }


    private void InputMove()
    {
        if (crash < crashLimit)
        {
            if(crash <= 0.05f)
            {
                GetComponent<SpriteRenderer>().sprite = psp;
            }
            else if ((int)(crash * 10) % 2 == 0)
            {
                GetComponent<SpriteRenderer>().sprite = null;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = psp;
            }
            return;
        }

        if (MyPlayer.name == "Player1")
        {
            // 上方向に向く
            if (Input.GetAxisRaw("Joystick 1 Vertical") > 0.1f && Mathf.Abs(Input.GetAxisRaw("Joystick 1 Vertical")) >= Mathf.Abs(Input.GetAxisRaw("Joystick 1 Horizontal")))
            {
                MyPlayer.rot = 0;
            }
            // 下方向に向く
            else if (Input.GetAxisRaw("Joystick 1 Vertical") < -0.1f && Mathf.Abs(Input.GetAxisRaw("Joystick 1 Vertical")) >= Mathf.Abs(Input.GetAxisRaw("Joystick 1 Horizontal")))
            {
                MyPlayer.rot = 1;
            }
            // 右方向に向く
            else if (Input.GetAxisRaw("Joystick 1 Horizontal") > 0.1f)
            {
                MyPlayer.rot = 2;
            }
            // 左方向に向く
            else if (Input.GetAxisRaw("Joystick 1 Horizontal") < -0.1f)
            {
                MyPlayer.rot = 3;
            }
            // 十字キー
            if (Input.GetAxisRaw("Joysticks 1 Vertical") > 0.1f && Mathf.Abs(Input.GetAxisRaw("Joysticks 1 Vertical")) >= Mathf.Abs(Input.GetAxisRaw("Joysticks 1 Horizontal")))
            {
                // 引き動作がオンだったら
                if (ispulling)
                {
                    MyPlayer.PullMove(1, pullWallN);
                }
                else
                {
                    // 下方向に向く
                    MyPlayer.Move(1);
                }
            }
            else if (Input.GetAxisRaw("Joysticks 1 Vertical") < -0.1f && Mathf.Abs(Input.GetAxisRaw("Joysticks 1 Vertical")) >= Mathf.Abs(Input.GetAxisRaw("Joysticks 1 Horizontal")))
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(0, pullWallN);
                }
                else
                {
                    // 上方向に向く
                    MyPlayer.Move(0);
                }
            }
            else if (Input.GetAxisRaw("Joysticks 1 Horizontal") > 0.1f)
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(2, pullWallN);
                }
                else
                {
                    // 右方向に向く
                    MyPlayer.Move(2);
                }
            }
            else if (Input.GetAxisRaw("Joysticks 1 Horizontal") < -0.1f)
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(3, pullWallN);
                }
                else
                {
                    // 左方向に向く
                    MyPlayer.Move(3);
                }
            }

            // Debug用入力
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(0, pullWallN);
                }
                else
                {
                    MyPlayer.Move(0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                if (ispulling)
                {
                    MyPlayer.PullMove(1, pullWallN);
                }
                else
                {
                    MyPlayer.Move(1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(2, pullWallN);
                }
                else
                {
                    MyPlayer.Move(2);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(3, pullWallN);
                }
                else
                {
                    MyPlayer.Move(3);
                }
            }

        }
        else if (MyPlayer.name == "Player2")
        {
            if (Input.GetAxisRaw("Joysticks 2 Vertical") > 0.1f && Mathf.Abs(Input.GetAxisRaw("Joysticks 2 Vertical")) >= Mathf.Abs(Input.GetAxisRaw("Joysticks 2 Horizontal")))
            {
                // 引き動作がオンの時
                if (ispulling)
                {
                    MyPlayer.PullMove(1, pullWallN);
                }
                else
                {
                    // 下方向に向く
                    MyPlayer.Move(1);
                }
            }
            else if (Input.GetAxisRaw("Joysticks 2 Vertical") < -0.1f && Mathf.Abs(Input.GetAxisRaw("Joysticks 2 Vertical")) >= Mathf.Abs(Input.GetAxisRaw("Joysticks 2 Horizontal")))
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(0, pullWallN);
                }
                else
                {
                    // 上方向に向く
                    MyPlayer.Move(0);
                }
            }
            else if (Input.GetAxisRaw("Joysticks 2 Horizontal") > 0.1f)
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(2, pullWallN);
                }
                else
                {
                    // 右方向に向く
                    MyPlayer.Move(2);
                }
            }
            else if (Input.GetAxisRaw("Joysticks 2 Horizontal") < -0.1f)
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(3, pullWallN);
                }
                else
                {
                    // 左方向に向く
                    MyPlayer.Move(3);
                }
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(0, pullWallN);
                }
                else
                {
                    MyPlayer.Move(0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {

                if (ispulling)
                {
                    MyPlayer.PullMove(1, pullWallN);
                }
                else
                {
                    MyPlayer.Move(1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(2, pullWallN);
                }
                else
                {
                    MyPlayer.Move(2);
                }
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                if (ispulling)
                {
                    MyPlayer.PullMove(3, pullWallN);
                }
                else
                {
                    MyPlayer.Move(3);
                }
            }
        }
    }

    private void ActionInput()
    {

        if (Recastlimit >= Recast) return;
        int num = (MyPlayer.name == "Player1") ? 1 : 2;

        if (Input.GetKeyDown("joystick " + num.ToString() + " button 2"))
        {
            MapKind P = (MyPlayer.name == "Player1") ? MapKind.Player1 : MapKind.Player2;
            MapKind A = (P == MapKind.Player1) ? MapKind.BombAria1 : MapKind.BombAria2;

            
            if (Isbomb)
            {
                Recast = 0;
                isbomb = false;
                // 爆弾を置く
                Map.instance.BombAria(MyPlayer.rot, P);
            }
            else
            {
                isbomb = true;
                // 爆弾範囲をだす
                Map.instance.AriaSet(P, A);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MapKind P = (MyPlayer.name == "Player1") ? MapKind.Player1 : MapKind.Player2;
            MapKind A = (P == MapKind.Player1) ? MapKind.BombAria1 : MapKind.BombAria2;


            if (Isbomb)
            {
                Recast = 0;
                isbomb = false;
                // 爆弾を置く
                Map.instance.BombAria(MyPlayer.rot, P);
            }
            else
            {
                isbomb = true;
                // 爆弾範囲をだす
                Map.instance.AriaSet(P, A);
            }
        }
    }
    

    private void WallGrab()
    {
        int num = (MyPlayer.name == "Player1") ? 1 : 2;
        if (Input.GetKeyDown("joystick " + num.ToString() + " button 3"))
        {
            Grab gr = GetComponent<Grab>();
            ispulling = gr.PullWall(ref pullWallN);
            Debug.Log("壁->"+(MapKind)pullWallN);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grab gr = GetComponent<Grab>();
            ispulling = gr.PullWall(ref pullWallN);
        }
        if(Input.GetKeyUp("joystick " + num.ToString() + " button 3"))
        {
            ispulling = false;
        }

    }

    public void BombCrash()
    {
        crash = 0;
    }
    
}
