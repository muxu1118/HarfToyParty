using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private Vector3 v3 = new Vector3();
    private PlayerInput PI;
    public Vector2 BomPos = new Vector2();
    
    

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
    /// ボム範囲を出すスクリプト
    /// </summary>
    /// <param name="player"></param>
    /// <param name="Aria"></param>
    /// <param name="BombPos"></param>
    /// <returns></returns>
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
                        if(i + x > 6 || i + x < 0 || Map.instance.mapInt[j, i + x] != (int)MapKind.YUKA)
                        {
                            if ((i + x > 6&&j == Map.instance.HWarpPoint[0].y )&& Map.instance.mapInt[(int)Map.instance.HWarpPoint[0].y, (int)Map.instance.HWarpPoint[0].x] == (int)MapKind.YUKA) {
                                x = (int)Map.instance.HWarpPoint[0].x;
                                gameObject.transform.position = new Vector3(Map.instance.SpritePos[j][x].x, Map.instance.SpritePos[j][x].y, 1);
                                if(player == MapKind.Player1) { Map.instance.BombPos1 = new Vector2((int)Map.instance.HWarpPoint[0].x, (int)Map.instance.HWarpPoint[0].y); }
                                else { Map.instance.BombPos2 = new Vector2((int)Map.instance.HWarpPoint[0].x, (int)Map.instance.HWarpPoint[0].y); }
                                BomPos = new Vector2((int)Map.instance.HWarpPoint[0].x, (int)Map.instance.HWarpPoint[0].y);
                                continue;
                            }
                            else if ((i + x < 0 && j == Map.instance.HWarpPoint[1].y) && Map.instance.mapInt[(int)Map.instance.HWarpPoint[1].y, (int)Map.instance.HWarpPoint[1].x] == (int)MapKind.YUKA) {
                                x = (int)Map.instance.HWarpPoint[1].x;
                                gameObject.transform.position = new Vector3(Map.instance.SpritePos[j][x].x, Map.instance.SpritePos[j][x].y, 1);
                                if (player == MapKind.Player1) { Map.instance.BombPos1 = new Vector2((int)Map.instance.HWarpPoint[1].x, (int)Map.instance.HWarpPoint[1].y); }
                                else { Map.instance.BombPos1 = new Vector2((int)Map.instance.HWarpPoint[1].x, (int)Map.instance.HWarpPoint[1].y); }
                                BomPos = new Vector2((int)Map.instance.HWarpPoint[1].x, (int)Map.instance.HWarpPoint[1].y);
                                continue;
                            }
                            else { x = 0; }
                        }
                        if (j + y > 6 || j + y < 0 || Map.instance.mapInt[j + y, i] != (int)MapKind.YUKA) {
                            if ((j + y > 6 && i == Map.instance.WarpPoint[0].x) && Map.instance.mapInt[(int)Map.instance.WarpPoint[0].y, (int)Map.instance.WarpPoint[0].x] == (int)MapKind.YUKA)
                            {
                                y = (int)Map.instance.WarpPoint[0].y;
                                gameObject.transform.position = new Vector3(Map.instance.SpritePos[y][i].x, Map.instance.SpritePos[y][i].y, 1);
                                if (player == MapKind.Player1) { Map.instance.BombPos1 = new Vector2((int)Map.instance.WarpPoint[0].x, (int)Map.instance.WarpPoint[0].y); }
                                else { Map.instance.BombPos2 = new Vector2((int)Map.instance.WarpPoint[0].x, (int)Map.instance.WarpPoint[0].y); }
                                continue;
                            }
                            else if ((j + y < 0 && i == Map.instance.WarpPoint[1].x) && Map.instance.mapInt[(int)Map.instance.WarpPoint[1].y, (int)Map.instance.WarpPoint[1].x] == (int)MapKind.YUKA)
                            {

                                y = (int)Map.instance.WarpPoint[1].y;
                                gameObject.transform.position = new Vector3(Map.instance.SpritePos[y][i].x, Map.instance.SpritePos[y][i].y, 1);
                                if (player == MapKind.Player1) { Map.instance.BombPos1 = new Vector2((int)Map.instance.WarpPoint[1].x, (int)Map.instance.WarpPoint[1].y); }
                                else { Map.instance.BombPos2 = new Vector2((int)Map.instance.WarpPoint[1].x, (int)Map.instance.WarpPoint[1].y); }
                                continue;
                            }
                            else { y = 0; }

                        }
                        gameObject.transform.position = new Vector3(Map.instance.SpritePos[j + y][i + x].x, Map.instance.SpritePos[j + y][i + x].y, 1);
                        if (player == MapKind.Player1) { Map.instance.BombPos1 = new Vector2(i + x, j + y); }
                        else { Map.instance.BombPos2 = new Vector2(i + x, j + y); }
                        
                    }
                        
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }


}
