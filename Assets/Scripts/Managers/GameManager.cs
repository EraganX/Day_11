using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private PlayerController player;

    [SerializeField] private TMP_Text scoreText;
    private float score;

    private bool isOver = false;
    private bool isStart = false;

    

    private void Awake()
    {
        Time.timeScale = 0f;
        player.isGameOver = false;
        isStart = true;
        startPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }


    public void GameOver()
    {
        startPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit(); 
    }

    private void Update()
    {
        if (player.isGameOver && isOver==false)
        {
            isOver = true;
            StartCoroutine(DelayOver());

        }

        if (!player.isGameOver && isStart)
        {
            score += Time.deltaTime;
            scoreText.text = score.ToString("00");
        }

    }

    IEnumerator DelayOver()
    {
        yield return new WaitForSeconds(2f);
        GameOver();

    }
}
