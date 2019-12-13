﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameRule
{
    public int PartGet;
    public int Time;
    public int Stage;
}


public class GameManager : SingletonMonoBehaviour<GameManager>
{
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
        GameRule game;
        game.PartGet = 1;
        game.Stage = 0;
        game.Time = 300;
        gameRule = game;
        
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
            Debug.Log("RedWin");
            GameState = State.Result;
        }
        if (BluePartGet == gameRule.PartGet && GameState == State.Main)
        {
            Debug.Log("BlueWin");
            GameState = State.Result;
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
                GameState = State.Main;
                break;
            default:
                break;
        }
        return;
    }
}
