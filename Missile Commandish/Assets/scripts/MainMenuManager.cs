using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/*******************************************************************************
 * class MainMenuManager *
 * Handles Main Menu functionality.
*******************************************************************************/
public class MainMenuManager : MonoBehaviour
  {
  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  /*****************************************************************************
   * Start
  *****************************************************************************/
  protected void Start()
    {  
    }

  /*****************************************************************************
   * Update
  *****************************************************************************/
  protected void Update()
    {
    if(Input.GetKeyDown(KeyCode.Escape))
      {
      quit();
      }
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * highScores *
   * Goes to the high score scene.
  *****************************************************************************/
  public void highScores()
    {
    Debug.Log("menu::highScores");
    SceneManager.LoadScene("HighScoreScene");
    }

  /*****************************************************************************
   * playGame *
   * Starts a new game.
  *****************************************************************************/
  public void playGame()
    {
    Debug.Log("menu::playGame");
    PlayerPrefs.SetInt("ContinuingGame", 0);
    SceneManager.LoadScene("MainGameScene");
    }

  /*****************************************************************************
   * quit *
   * Quits the game.
  *****************************************************************************/
  public void quit()
    {
    Debug.Log("menu::quit");
    PlayerPrefs.SetInt("ContinuingGame", 0);
    Application.Quit();
    }
  }
