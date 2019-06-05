using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField] private Text timeLabel;
    private PlayerLook playerLook;
    public GameObject pauseMenu;
    private Vector3 initPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Transform player;

    private DateTime startTime;
    private bool gameRunning = false;
    float timer = 0.0f;
    int timeUsed;

    // Start is called before the first frame update
    void Start()
    {
        playerLook = GameObject.Find("PlayerCamera").GetComponent<PlayerLook>();
        pauseMenu.SetActive(false);
        initPosition.x = player.position.x;
        initPosition.y = player.position.y;
        initPosition.z = player.position.z;
    }

    public void StartNewGame()
    {
        gameRunning = true;
        timer = 0.0f;
        timeUsed = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FinishGame()
    {
        gameRunning = false;
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        playerLook.lookEnabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameRunning = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void DeactivatePause()
    {
        playerLook.lookEnabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameRunning = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameRunning)
            {
                PauseGame();
            }
            else
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
