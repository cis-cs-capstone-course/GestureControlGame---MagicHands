﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;
using Photon.Pun;

public class PauseMenu : MonoBehaviour
{
    private Dictionary<string, Action> keyActs = new Dictionary<string, Action>();

    private KeywordRecognizer recognizer;
    public static bool isPaused = false;


    public GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);

        keyActs.Add("stop", Pause);
        keyActs.Add("resume", Resume);
        keyActs.Add("options", Options);
        keyActs.Add("quit", QuitGame);
        recognizer = new KeywordRecognizer(keyActs.Keys.ToArray());
        recognizer.OnPhraseRecognized += OnPhraseRecognized;
        recognizer.Start();
    }

    void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Command: " + args.text + " " + Time.time);
        keyActs[args.text].Invoke();
    }
  
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }
    public void Resume()
    {
        if(isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Options()
    {   
        if(isPaused)
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1f;
            isPaused = false;
        }

    }
    public void QuitGame()
    {   
        if(isPaused)
        {
            if(PhotonNetwork.InRoom)
                PhotonNetwork.LeaveRoom(true);
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu2");
            Debug.Log("Quitting Scene");
            //Application.Quit();
        }

    }
    

    public KeywordRecognizer GetVoiceControl()
    {
        return recognizer;
    }
}
