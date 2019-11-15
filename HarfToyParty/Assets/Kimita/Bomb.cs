using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Tap
{
    private GameObject Player;
    
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
        
    }

    public void BombPut()
    {
        if (Player.gameObject.name == "Player1")
        {
            gameObject.SetActive(false);
            Map.instance.BombAria(Player.GetComponent<Player>().rot, MapKind.Player1);
        }
    }
    public IEnumerator AriaSet(MapKind player, MapKind Aria,Vector2 BombPos)
    {
        Debug.Log(player.ToString());
        Player = GameObject.Find(player.ToString());
            while (gameObject.activeSelf)
            {
                int x = 0, y = 0;
                switch (Player.GetComponent<Player>().rot)
                {
                    case 0://上
                        y = -1;
                        break;
                    case 1://下
                        y = 1;
                        break;
                    case 2://右
                        x = 1;
                        break;
                    case 3://左
                        x = -1;
                        break;
                }
                for (int i = 0; i <= 6; i++)
                {
                    for (int j = 0; j <= 6; j++)
                    {
                        if (Map.instance.mapInt[j, i] == (int)player)
                        {
                            if (i + x > 6 || i + x < 0) { x = 0; }
                            if (j + y > 6 || j + y < 0) { y = 0; }
                            gameObject.transform.position = new Vector3(Map.instance.SpritePos[j + y][i + x].x, Map.instance.SpritePos[j + y][i + x].y, 1);
                            Map.instance.BombPos1 = new Vector2(i + x, j + y);
                        }
                    }
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }


}
