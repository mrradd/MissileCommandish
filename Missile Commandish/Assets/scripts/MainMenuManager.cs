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
  protected void Start()
    {  
    long x = DateTime.Now.Ticks;
    long y = Convert.ToInt64(PlayerPrefs.GetString("LastTimePlayed"));
    DateTime yy = new DateTime(y);
    DateTime xx = new DateTime(x);
    TimeSpan xy = xx - yy;
    Debug.Log(xy.Minutes);
    PlayerPrefs.SetString("LastTimePlayed", DateTime.Now.Ticks.ToString());
    }

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
   * playGame *
   * Starts a new game.
  *****************************************************************************/
  public void playGame()
    {
    Debug.Log("playGame");
    SceneManager.LoadScene("MainGameScene");
    }

  /*****************************************************************************
   * quit *
   * Quits the game.
  *****************************************************************************/
  public void quit()
    {
    Debug.Log("quit");
    PlayerPrefs.SetInt("CameFromGameOverScreen", 0);
    Application.Quit();
    }
  }
