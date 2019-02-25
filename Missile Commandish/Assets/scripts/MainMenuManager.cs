using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*******************************************************************************
 * class MainMenuManager *
 * Handles Main Menu functionality.
*******************************************************************************/
public class MainMenuManager : MonoBehaviour
  {
  /*****************************************************************************
   * Unity Methods
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
    Application.Quit();
    }
  }
