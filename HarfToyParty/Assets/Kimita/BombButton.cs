using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Tap
{
    public GameObject Player;
    
    private void Start()
    {
        
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


}
