using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapKind
{
    YUKA = 0,
    Bomb,
    Movewall,
    Player1,
    Player2
}

public class Map : SingletonMonoBehaviour<Map>
{
    List<List<Vector3>> spritePos = new List<List<Vector3>>();
    public int[,] mapInt = new int[7,7];

    public List<MapKind> maps = new List<MapKind>();
    [SerializeField]
    private GameObject[] MapObject = new GameObject[System.Enum.GetNames(typeof(MapKind)).Length];
    bool isMove;
    private void Start()
    {
        // マップを探す
        foreach (Transform Y in gameObject.transform)
        {
            Debug.Log(Y.name);
            spritePos.Add(new List<Vector3>());
            for (int i = 0; i <= 6; i++)
            {
                if (Y.name == "Y" + i)
                {
                    foreach (Transform X in Y.transform)
                    {
                        spritePos[i].Add(X.transform.position);
                        mapInt[i, i] = 0;

                    }
                }
            }
        }
        for (int i = 0; i < System.Enum.GetNames(typeof(MapKind)).Length; i++)
        {
            maps.Add((MapKind)i);
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i <= 6; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    Debug.Log("X:" + i + "Y:" + j + "     " + spritePos[i][j]);
                    Debug.Log("X:" + i + "Y:" + j + "     " + mapInt[i, j]);
                }
            }
        }
    }

    private void CheckMap()
    {
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                for (int l = 0; l < System.Enum.GetNames(typeof(MapKind)).Length; l++)
                {
                    if(mapInt[j,i] == (int)maps[l])
                    {
                        MapObject[(int)maps[l]].transform.position = spritePos[i][j];
                    }

                }
            }
        }
    }
    public void Move(MapKind map, Vector2 vec2)
    {
        if (isMove) return;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                
                if (mapInt[i, j] == (int)map && (j+(int)vec2.x >= 6|| j - (int)vec2.x < 0 || i-(int)vec2.y >= 6|| i + (int)vec2.y <0)) 
                {
                    return;
                }
            }
        }
        isMove = true;
        StartCoroutine(MoveAnim(2, map, vec2));
    }
    IEnumerator MoveAnim(float wait,MapKind map,Vector2 vec2)
    {
        float time = wait / Time.deltaTime;
        int x=0, y=0;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if (mapInt[i, j] == (int)map && (i + (int)vec2.x >= 6 || i - (int)vec2.x < 0 || j - (int)vec2.y >= 6 || j + (int)vec2.y < 0))
                {
                    x = i;
                    y = j;
                }
            }
        }
        Debug.Log(spritePos[y - (int)vec2.y][x + (int)vec2.x]);
        while (wait >= 0)
        {
            wait -= Time.deltaTime;
            MapObject[(int)map].transform.position += new Vector3((spritePos[y][x].x - spritePos[y - (int)vec2.y][x + (int)vec2.x].x) / time, (spritePos[y - (int)vec2.y][x + (int)vec2.x].y-spritePos[y][x].y) / time);
            //MapObject[(int)map].transform.position += new Vector3((MapObject[(int)map].transform.position.x-spritePos[y - (int)vec2.y][x + (int)vec2.x].x) / time , (spritePos[y - (int)vec2.y][x + (int)vec2.x].y - MapObject[(int)map].transform.position.y  ) / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        MapObject[(int)map].transform.position = new Vector3(spritePos[y - (int)vec2.y][x + (int)vec2.x].x, spritePos[y - (int)vec2.y][x + (int)vec2.x].y);
        mapInt[x, y] = 0;
        mapInt[x + (int)vec2.x, y - (int)vec2.y] = (int)map;
        isMove = false;
    }

}
