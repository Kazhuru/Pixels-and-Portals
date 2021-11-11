using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int MaxLives;
    [SerializeField] int coinValue;
    [SerializeField] AudioClip gameMusic;
    [SerializeField][Range(0f,1f)] float gameMusicVolume = 1f;

    private int currentLevel;
    private int currentPlayerLives;
    private int currentScore;

    public int CurrentPlayerLives { get => currentPlayerLives; set => currentPlayerLives = value; }
    public int CurrentScore { get => currentScore; set => currentScore = value; }
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    private void Awake()
    {
        int gameManagerCounter = FindObjectsOfType<GameSession>().Length;
        if (gameManagerCounter > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        RestartSessionVariables();
        SetUpGameMusic();
    }

    private void RestartSessionVariables()
    {
        currentLevel = 0;
        currentPlayerLives = MaxLives;
        currentScore = 0;
    }

    private void SetUpGameMusic()
    {
        AudioSource MyAudioSource = GetComponent<AudioSource>();
        if (gameMusic)
        {
            MyAudioSource.clip = gameMusic;
            MyAudioSource.volume = gameMusicVolume;
            MyAudioSource.Play();
        }
    }

    public void playerDeathManage()
    {
        
        if(currentPlayerLives > 0)
        {
            currentPlayerLives--;
            FindObjectOfType<SceneLoader>().RestartScene();
        }
        else
        {
            FindObjectOfType<SceneLoader>().LoadGameoverScene();
        }
    }

    public void IncreaseScore()
    {
        CurrentScore += coinValue;
    }
}
