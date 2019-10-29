using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerManager
{
    private int myNumber;
    private void Start()
    {
        map = Map.instance;
        myNumber = (gameObject.name == "Player1") ? (int)MapKind.Player1: (int)MapKind.Player2;
        map.mapInt[1, 0] = myNumber;
    }

    private void Update()
    {
        
    }

    public void Move(int x)
    {
        switch (x)
        {
            case 0:
                map.Move((MapKind)myNumber, new Vector2(0, 1));
                break;
            case 1:
                map.Move((MapKind)myNumber, new Vector2(0, -1));
                break;
            case 2:
                map.Move((MapKind)myNumber, new Vector2(1, 0));
                break;
            case 3:
                map.Move((MapKind)myNumber, new Vector2(-1, 0));
                break;

        }
    }



}
