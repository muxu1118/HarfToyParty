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
        //StageSelect(2);
    }
    /// <summary>
    /// ステージを選択する
    /// </summary>
    /// <param name="num"></param>
    public void StageSelect(int num)
    {
        StartCoroutine(MapDelayCreate(num));
    }

    public void StageReset()
    {
        Destroy(GameObject.Find("Tutorial"));
        Destroy(GameObject.Find("Map1"));
        Destroy(GameObject.Find("Map2"));
        Destroy(GameObject.Find("Map3"));
        Destroy(GameObject.Find("Map4"));
        Destroy(GameObject.Find("Map5"));
    }

    IEnumerator MapDelayCreate(int num)
    {
        yield return new WaitForSeconds(0.1f);
        switch (num)
        {
            case 0:
                Instantiate(Map[num]).name = "Map1";
                break;
            case 1:
                Instantiate(Map[num]).name = "Map2";
                break;
            case 2:
                Instantiate(Map[num]).name = "Map3";
                break;
            case 3:
                Instantiate(Map[num]).name = "Map4";
                break;
            case 4:
                Instantiate(Map[num]).name = "Map5";
                break;
        }

    }


}
