using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSwitching : MonoBehaviour
{
    [SerializeField]
    GameObject[] gameMap;
    int[] map;
    int mapNumber;

    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < gameMap.Length; i++)
        {
            int RandomMap = map[i];
            int random = Random.Range(i, gameMap.Length);
            //map[i] = 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MapDesplay()
    {
        gameMap[mapNumber].SetActive(true);
    }
}
