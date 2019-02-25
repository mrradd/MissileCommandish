using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************************************************************************
 * class PauseMenuUIManager *
 * Handles Pause Menu functionality.
*******************************************************************************/
public class PauseMenuUIManager : MonoBehaviour
  {
  
  /** Player's current score. */
  public Text currentScoreText;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  protected void Update()
    {
    if(Input.GetKeyDown(KeyCode.Escape))
      {
      handleResume();
      }
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * handleMainMenu *
   * Handler for when the Main Menu button is pressed.
  *****************************************************************************/
  public void handleMainMenu()
    {
    Debug.Log("pm::handleMainMenu");
    GameManager.mainMenu();
    }

  /*****************************************************************************
   * handleResume *
   * Handler for when the Resume button is pressed.
  *****************************************************************************/
  public void handleResume()
    {
    Debug.Log("pm::handleResume");
    GameManager.toggleCamera(1);
    }

  /*****************************************************************************
   * handleResume *
   * Handler for when the Restart button is pressed.
  *****************************************************************************/
  public void handleRestart()
    {
    Debug.Log("pm::handleRestart");
    GameManager.restartGame();
    }

  /*****************************************************************************
   * updateText *
   * Updates the UI text.
  *****************************************************************************/
  public void updateText()
    {
    currentScoreText.text = "Current Score\n" + GameManager.instance.playerScore;
    }
  }
