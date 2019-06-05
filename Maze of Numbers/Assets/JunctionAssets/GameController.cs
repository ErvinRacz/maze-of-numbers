using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text timeLabel;
    private PlayerLook playerLook;
    public GameObject pauseMenu;
    public GameObject congratText;
    private Vector3 initPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Transform player;

    private DateTime startTime;
    private bool gameRunning = false;
    private bool win = false;
    float timer = 0.0f;
    int timeUsed;

    // Start is called before the first frame update
    void Start()
    {
        // Later we want to pause the movement of the player when he hits the escape button (for the pause menu).
        playerLook = GameObject.Find("PlayerCamera").GetComponent<PlayerLook>();
        pauseMenu.SetActive(false);
        congratText.SetActive(false);

        // Add a initial random rotation to the player
        var euler = player.eulerAngles;
        euler.y = Random.Range(0.0f, 360.0f);
        player.eulerAngles = euler;
    }

    public void StartNewGame()
    {
        // game starts to run, so its time to reset the timers
        gameRunning = true;
        timer = 0.0f;
        timeUsed = 0;
        Cursor.visible = false; // hide the cursore, since this is an fps game
        Cursor.lockState = CursorLockMode.Locked; // mouse cursor cannot be used if its locked
    }

    public void FinishGame()
    {
        gameRunning = false;
        win = true;
        PauseGame();
        // Time to show the "congratulations" text
        congratText.SetActive(true);
        // Hide the resume button, since the level is completed
        var resumeBtn = GameObject.Find("Resume Button");
        resumeBtn.SetActive(false);
    }

    public void PauseGame()
    {
        // block the character movements
        playerLook.lookEnabled = false;
        // show the cursor to interact with the menu buttons
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameRunning = false;
        pauseMenu.SetActive(true);
        // time scales down to 0 - time is frozen in this state
        Time.timeScale = 0;
    }

    public void DeactivatePause()
    {
        // the player can move again
        playerLook.lookEnabled = true;
        // hide and lock the mouse cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameRunning = true;
        pauseMenu.SetActive(false);
        // time scales up to 1 - time works regurarly in this state
        Time.timeScale = 1;
    }

    public void Restart()
    {
        // the scene is reloaded when the player hits the restart button, so
        // everything will be reinitialized. It is easyer then resetting the state of the game manually.
        DeactivatePause();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }

    public void SelectLevel()
    {
        DeactivatePause();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Logic controlling the menu and updating the timer
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameRunning)
            {
                PauseGame();
            }
            else if(!win)
            {
                DeactivatePause();
            }
        }
        
        if (gameRunning)
        {
            timer += Time.deltaTime;
            timeUsed = (int) (timer % 60);
        }

        timeLabel.text = timeUsed.ToString() + " sec";
    }
}
