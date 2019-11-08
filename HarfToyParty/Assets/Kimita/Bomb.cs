﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Tap
{
    private GameObject Player;
    
    private void Start()
    {
        Player = transform.root.gameObject;
    }
    private void Update()
    {
        
    }
    /// <summary>
    /// ボムの範囲チェック
    /// </summary>
    public void BombRange()
    {
        // 
        if(Player.gameObject.name == "Player1")
        {
            Map.instance.BombAria(Player.GetComponent<Player>().rot,MapKind.Player1);

        }else if (Player.gameObject.name == "Player2")
        {
            Map.instance.BombAria(Player.GetComponent<Player>().rot, MapKind.Player2);
        }
    }

    public void BombPut()
    {
        if (Player.gameObject.name == "Player1")
        {
            gameObject.SetActive(false);
            Map.instance.BombAria(Player.GetComponent<Player>().rot, MapKind.Player1);
        }
    }
    


}
