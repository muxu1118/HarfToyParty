using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Tap
{
    [SerializeField]
    private GameObject Player;
    private Vector3 v3 = new Vector3();
    private PlayerInput PI;

    private void Start()
    {
        v3 = transform.position;
        PI = Player.GetComponent<PlayerInput>();
    }
    private void Update()
    {
        if (!PI.Isbomb)
        {
            transform.position = v3;
        }
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
        else
        {
            gameObject.SetActive(false);
            Map.instance.BombAria(Player.GetComponent<Player>().rot, MapKind.Player2);
        }
    }

    public IEnumerator AriaSet(MapKind player, MapKind Aria,Vector2 BombPos)
    {
        while (gameObject.activeSelf&&PI.Isbomb)
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
                        if (i + x > 6 || i + x < 0 || Map.instance.mapInt[j, i + x] != (int)MapKind.YUKA) { x = 0; }
                        if (j + y > 6 || j + y < 0 || Map.instance.mapInt[j + y, i] != (int)MapKind.YUKA) { y = 0; }
                        gameObject.transform.position = new Vector3(Map.instance.SpritePos[j + y][i + x].x, Map.instance.SpritePos[j + y][i + x].y, 1);
                        Map.instance.BombPos1 = new Vector2(i + x, j + y);
                    }
                        
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }


}
