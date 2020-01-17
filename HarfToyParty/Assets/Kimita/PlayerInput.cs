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
    Sprite psp;//PlayerのSprite

    // Start is called before the first frame update
    void Start()
    {
        float x = Input.GetAxisRaw("Horizontal");
        isbomb = false;
        crash = 0.9f;
        psp = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        InputMove();
        ActionInput();
        TestInput();
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
            if (Input.GetAxisRaw("Joystick 1 Vertical") > 0.1f && Mathf.Abs(Input.GetAxisRaw("Joystick 1 Vertical")) >= Mathf.Abs(Input.GetAxisRaw("Joystick 1 Horizontal")))
            {
                MyPlayer.Move(0);
            }
            else if (Input.GetAxisRaw("Joystick 1 Vertical") < -0.1f && Mathf.Abs(Input.GetAxisRaw("Joystick 1 Vertical")) >= Mathf.Abs(Input.GetAxisRaw("Joystick 1 Horizontal")))
            {
                MyPlayer.Move(1);
            }
            else if (Input.GetAxisRaw("Joystick 1 Horizontal") > 0.1f)
            {
                MyPlayer.Move(2);
            }
            else if (Input.GetAxisRaw("Joystick 1 Horizontal") < -0.1f)
            {
                MyPlayer.Move(3);
            }
        }
        else if (MyPlayer.name == "Player2")
        {
            if (Input.GetAxisRaw("Joystick 2 Vertical") > 0.1f && Mathf.Abs(Input.GetAxisRaw("Joystick 2 Vertical")) >= Mathf.Abs(Input.GetAxisRaw("Joystick 2 Horizontal")))
            {
                MyPlayer.Move(0);
            }
            else if (Input.GetAxisRaw("Joystick 2 Vertical") < -0.1f && Mathf.Abs(Input.GetAxisRaw("Joystick 2 Vertical")) >= Mathf.Abs(Input.GetAxisRaw("Joystick 2 Horizontal")))
            {
                MyPlayer.Move(1);
            }
            else if (Input.GetAxisRaw("Joystick 2 Horizontal") > 0.1f)
            {
                MyPlayer.Move(2);
            }
            else if (Input.GetAxisRaw("Joystick 2 Horizontal") < -0.1f)
            {
                MyPlayer.Move(3);
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
    }

    private void TestInput()
    {
        int num = (MyPlayer.name == "Player1") ? 1 : 2;
        if (Input.GetKeyDown("joystick 1 button 0"))
        {
            Debug.Log("JoyStick0");
        }
        if (Input.GetKeyDown("joystick " + num.ToString() + " button 1"))
        {
            Debug.Log("JoyStick1");
        }
        if (Input.GetKeyDown("joystick " + num.ToString() + " button 2"))
        {
            Debug.Log("JoyStick2");
        }
        if (Input.GetKeyDown("joystick " + num.ToString() + " button 3"))
        {
            Debug.Log("JoyStick3");
        }
    }

    public void BombCrash()
    {
        crash = 0;
    }

    private void BombInput()
    {

    }
    
}
