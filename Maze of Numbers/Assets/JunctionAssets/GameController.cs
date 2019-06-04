using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    private MazeConstructor generator;

    [SerializeField] private Text timeLabel;
    [SerializeField] private Text scoreLabel;

    private DateTime startTime;
    private int timeLimit;
    private int reduceLimitBy;

    private int score;
    private bool goalReached;

    // Start is called before the first frame update
    void Start()
    {
        generator = GetComponent<MazeConstructor>();
        StartNewGame();
    }

    private void StartNewGame()
    {
        timeLimit = 80;
        reduceLimitBy = 5;
        startTime = DateTime.Now;

        score = 0;
        scoreLabel.text = score.ToString();

        StartNewMaze();
    }

    private void StartNewMaze()
    {
        //generator.GenerateNewMaze(1, OnStartTrigger, OnGoalTrigger);
        //player.transform.position = new Vector3(x, y, z);

        goalReached = false;
        //player.enabled = true;

        //restart timer
        timeLimit -= reduceLimitBy;
        startTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //if (!player.enabled){return;}
    
        int timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
        int timeLeft = timeLimit - timeUsed;

        if (timeLeft > 0)
        {
            timeLabel.text = timeLeft.ToString();
        }
        else
        {
            timeLabel.text = "TIME UP";
            // player.enabled = false;
            Invoke("StartNewGame", 4);
        }
        */
    }

    private void OnGoalTrigger(GameObject trigger, GameObject other)
    {
        Debug.Log("Goal!");
        goalReached = true;

        score += 1;
        scoreLabel.text = score.ToString();

        Destroy(trigger);
    }

    private void OnStartTrigger(GameObject trigger, GameObject other)
    {
        if (goalReached)
        {
            Debug.Log("Finish!");
            //player.enabled = false;

            Invoke("StartNewMaze", 4);
        }
    }
}
