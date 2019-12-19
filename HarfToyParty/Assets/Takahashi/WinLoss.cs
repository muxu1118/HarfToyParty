using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WinLoss : NetworkBehaviour
{
    [SerializeField]
    private int winnerDesplay;
    [SerializeField]
    private Text winnerText;
    Text ServerText;
    Text ClientText;

    void Start()
    {
        // これがホストだったら
        if (isServer)
        {
            // 1P側の変更
            ServerText = winnerText;
        }
        // これがクライアントだったら
        if (isClient)
        {
            // 2P側の変更
            ClientText = winnerText;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinOrLoss(int winner)
    {
        int winnerNum = winner;
        switch (winnerNum)
        {
            case 1:
                ServerText.text = "Winner";
                break;
            case 2:
                ClientText.text = "Winner";
                break;
        }        
    }
}
