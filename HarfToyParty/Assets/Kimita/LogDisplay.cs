using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogDisplay : MonoBehaviour
{
    public Text log;
    private void Awake()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void HandleLog(string logText, string stackTrace, LogType type)
    {
        log.text = logText;
    }
    public void PushButton()
    {
        if (!Input.GetKeyDown(KeyCode.A)) return;
        string str = "";
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                str += Map.instance.mapInt[i, j].ToString();
            }
            str += "\n";
        }
        
        Debug.Log(str);
    }
    private void Update()
    {
        PushButton();
    }
}
