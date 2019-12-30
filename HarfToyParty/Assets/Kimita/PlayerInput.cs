using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Player MyPlayer;
    private bool isbomb;

    public bool Isbomb { get => isbomb;}

    // Start is called before the first frame update
    void Start()
    {
        float x = Input.GetAxisRaw("Horizontal");
        isbomb = false;
    }

    // Update is called once per frame
    void Update()
    {
        InputMove();
        ActionInput();
    }


    private void InputMove()
    {
        if (MyPlayer.name == "Player1")
        {
            if (Input.GetAxisRaw("C1V") > 0.1f && Mathf.Abs(Input.GetAxisRaw("C1V")) >= Mathf.Abs(Input.GetAxisRaw("C1H")))
            {
                MyPlayer.Move(0);
            }
            else if (Input.GetAxisRaw("C1V") < -0.1f && Mathf.Abs(Input.GetAxisRaw("C1V")) >= Mathf.Abs(Input.GetAxisRaw("C1H")))
            {
                MyPlayer.Move(1);
            }
            else if (Input.GetAxisRaw("C1H") > 0.1f)
            {
                MyPlayer.Move(2);
            }
            else if (Input.GetAxisRaw("C1H") < -0.1f)
            {
                MyPlayer.Move(3);
            }
        }
        else if (MyPlayer.name == "Player2")
        {
            if (Input.GetAxisRaw("C2V") > 0.1f && Mathf.Abs(Input.GetAxisRaw("C2V")) >= Mathf.Abs(Input.GetAxisRaw("C2H")))
            {
                MyPlayer.Move(0);
            }
            else if (Input.GetAxisRaw("C2V") < -0.1f && Mathf.Abs(Input.GetAxisRaw("C2V")) >= Mathf.Abs(Input.GetAxisRaw("C2H")))
            {
                MyPlayer.Move(1);
            }
            else if (Input.GetAxisRaw("C2H") > 0.1f)
            {
                MyPlayer.Move(2);
            }
            else if (Input.GetAxisRaw("C2H") < -0.1f)
            {
                MyPlayer.Move(3);
            }
        }
    }

    private void ActionInput()
    {

        int num = (MyPlayer.name == "Player1") ? 1 : 2;
        if (Input.GetKeyDown("joystick " + num.ToString() + " button 0"))
        {
            MapKind P = (MyPlayer.name == "Player1") ? MapKind.Player1 : MapKind.Player2;
            MapKind A = (P == MapKind.Player1) ? MapKind.BombAria1 : MapKind.BombAria2;

            if (Isbomb)
            {
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

    private void BombInput()
    {

    }
    
}
