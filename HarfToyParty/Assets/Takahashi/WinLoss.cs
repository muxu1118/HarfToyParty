using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WinLoss : NetworkBehaviour
{
    //[SerializeField]
    //private int winnerDesplay;
    [SerializeField]
    private Text winnerText;
    [SerializeField]
    GameObject panel;
    string ServerText = "Loss";
    string ClientText = "Loss";

    void Start()
    {        
        // これがホストだったら
        if (isServer)
        {
            // 1P側の変更
            winnerText.text = ServerText;
            Debug.Log(winnerText.text);
        }
        // これがクライアントだったら
        if (isClient)
        {
            // 2P側の変更
            winnerText.text = ClientText;
            Debug.Log(winnerText.text);
        }
    }

    private void Update()
    {
        
    }

    public void WinOrLoss(int winner)
    {
        int winnerNum = winner;
        switch (winnerNum)
        {
            case 1:
                panel.SetActive(true);
                ServerText = "Win";
                winnerText.text = ServerText;
                break;
            case 2:
                panel.SetActive(true);
                ClientText = "Win";
                winnerText.text = ClientText;
                break;
        }        
    }
}
