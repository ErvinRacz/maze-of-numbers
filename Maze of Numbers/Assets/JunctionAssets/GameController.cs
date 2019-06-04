using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField] private Text timeLabel;

    private DateTime startTime;
    private bool gameRunning = false;
    int timeUsed;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartNewGame()
    {
        gameRunning = true;
        startTime = DateTime.Now;
    }

    public void FinishGame()
    {
        gameRunning = false;
        startTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
        }

        timeLabel.text = timeUsed.ToString() + " sec";
    }
}
