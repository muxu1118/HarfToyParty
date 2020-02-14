﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * SliderでTimerを作成する
 */
public class Timer : MonoBehaviour
{
    [SerializeField]
    private float count;
    private float timeLimit;
    //[SerializeField]
    private Slider timeSlider;
    private bool drowTrigger = true;
    private void Start()
    {
        //timeLimit = GameManager.instance.gameRule.Time;
        //timeLimit = 300;
        timeLimit = 300;
        count = 0;
        timeSlider = gameObject.GetComponent<Slider>();
    }
    private void Update()
    {
        TimeCount();
    }
    
    private void TimeCount()
    {
        if (drowTrigger)
        {
            if (timeLimit >= count)
            {
                count += Time.deltaTime;
                // 時間をSliderのValueに入れる
                timeSlider.value = 1 - (1f / (timeLimit / count));
            }
            else
            {
                // 引き分け動作
                //Debug.Log("TimeUp");
                GameManager.instance.DrawGame();
                drowTrigger = false;
            }
        }
        
    }
    public void TimeReset()
    {
        count = 0;
        drowTrigger = true;
    }
}
