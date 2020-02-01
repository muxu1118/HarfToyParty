using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : SingletonMonoBehaviour<Stage>
{
    public GameObject[] Map = new GameObject[3];
    [SerializeField]
    GameObject[] Player = new GameObject[2];

    private void Start()
    {
        StageSelect(2);
    }
    /// <summary>
    /// ステージを選択する
    /// </summary>
    /// <param name="num"></param>
    public void StageSelect(int num)
    {
        switch (num)
        {
            case 0:
                Instantiate(Map[num]);
                break;
            case 1:
                Instantiate(Map[num]);
                break;
            case 2:
                Instantiate(Map[num]);
                break;
        }
    }

    public void StageReset()
    {
        Destroy(GameObject.Find("Tutorial"));
        Destroy(GameObject.Find("Map1"));
        Destroy(GameObject.Find("Map2"));
    }



}
