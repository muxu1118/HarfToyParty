using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winLoseController : MonoBehaviour
{
    WinLoss winLoss;

    // Start is called before the first frame update
    void Start()
    {
        winLoss = GameObject.Find("ResultUI").GetComponent<WinLoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
