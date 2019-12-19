using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct GameRule
{
    public int PartGet;
    public int Time;
    public int Stage;
}


public class GameManager : SingletonMonoBehaviour<GameManager>
{
    WinLoss winLoss;
    enum State
    {
        Title,
        PlayerChoise,
        MapSelect,
        RuleSelect,
        Main,
        Result
    }

    State GameState;
    public GameRule gameRule = new GameRule();

    public int RedPartGet;
    public int BluePartGet;

    private void Start()
    {
        GameState = State.Main;
        GameRule game;
        game.PartGet = 1;
        game.Stage = 0;
        game.Time = 300;
        gameRule = game;
        winLoss = GameObject.Find("ResultUI").GetComponent<WinLoss>();
    }

    private void Reset()
    {
        RedPartGet = 0;
        BluePartGet = 0;
        
    }
    // Update is called once per frame
    void Update()
    {
        MainGame();   
    }

    private void MainGame()
    {
        if(RedPartGet == gameRule.PartGet&&GameState == State.Main)
        {
            winLoss = GameObject.Find("ResultUI").GetComponent<WinLoss>();
            Debug.Log("RedWin");
            GameState = State.Result;
            winLoss.WinOrLoss(1);
        }
        if (BluePartGet == gameRule.PartGet && GameState == State.Main)
        {
            winLoss = GameObject.Find("ResultUI").GetComponent<WinLoss>();
            Debug.Log("BlueWin");
            GameState = State.Result;
            winLoss.WinOrLoss(2);
        }
    }
    public void StateChange()
    {
        switch (GameState)
        {
            case State.Title:
                GameState = State.PlayerChoise;
                break;
            case State.PlayerChoise:
                GameState = State.Main;
                break;
            case State.Main:
                GameState = State.Result;
                break;
            case State.Result:
                GameState = State.PlayerChoise;
                break;
            default:
                break;
        }
        return;
    }
}
