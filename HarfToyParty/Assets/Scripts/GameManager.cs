﻿using System.Collections;
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
    public enum State
    {
        Title,
        Story,
        MapSelect,
        RuleSelect,
        Main,
        Result
    }
    [SerializeField]
    State GameState;
    public GameRule gameRule = new GameRule();

    public int RedPartGet;
    public int BluePartGet;

    private void Start()
    {
        //GameState = State.Main;
        //GameState = State.Title;
        //GameRule game;
        //game.PartGet = 1;
        //game.Stage = 0;
        //game.Time = 300;
        //gameRule = game;
        DontDestroyOnLoad(gameObject);
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
        // 赤が勝ったら
        if(RedPartGet == gameRule.PartGet&&GameState == State.Main)
        {
            winLoss = GameObject.Find("ResultUI").GetComponent<WinLoss>();
            Debug.Log("RedWin");
            GameState = State.Title;
            winLoss.WinOrLoss(1);
        }
        // 青が勝ったら
        if (BluePartGet == gameRule.PartGet && GameState == State.Main)
        {
            winLoss = GameObject.Find("ResultUI").GetComponent<WinLoss>();
            Debug.Log("BlueWin");
            GameState = State.Title;
            winLoss.WinOrLoss(2);
        }        
    }

    public void StateChange()
    {
        Debug.Log(GameState);
        switch (GameState)
        {
            case State.Title:
                GameState = State.Story;
                break;
            case State.Story:
                GameState = State.Main;
                break;
            case State.Main:
                GameState = State.Title;
                break;
            //case State.Result:
            //    GameState = State.Title;
            //    break;
            default:
                break;
        }
        return;
    }

    public void StateCall(State state)
    {
        GameState = state;
    }

    public void startScene()
    {
        GameState = State.Title;
        GameRule game;
        game.PartGet = 1;
        game.Stage = 0;
        game.Time = 300;
        gameRule = game;

    }    
}