using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************************************************************************
 * class LevelClearedUIManager *
 * Handles Level Cleared menu functionality.
*******************************************************************************/
public class LevelClearedUIManager : MonoBehaviour
  {
  /** Player's current score. */
  public Text totalScoreText;

  /** Bonus from cities. */
  public Text cityBonusText;

  /** Bonus from launchers. */
  public Text launcherBonusText;

  /** Bonus from rockets. */
  public Text rocketBonusText;

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * handleQuit *
   * Handler for when the Quit button is pressed.
  *****************************************************************************/
  public void handleQuit()
    {
    Debug.Log("handleMainMenu");
    }

  /*****************************************************************************
   * handleContinue *
   * Handler for when the Continue button is pressed.
  *****************************************************************************/
  public void handleContinue()
    {
    GameManager.startNextLevel();
    }

  /*****************************************************************************
   * handleRestart *
   * Handler for when the Restart button is pressed.
  *****************************************************************************/
  public void handleRestart()
    {
    GameManager.restartGame();
    }
  }
