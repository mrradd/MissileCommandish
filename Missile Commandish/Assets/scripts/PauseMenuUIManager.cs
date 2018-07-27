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
   * handleMainMenu *
   * Handler for when the Main Menu button is pressed.
  *****************************************************************************/
  public void handleMainMenu()
    {
    Debug.Log("handleMainMenu");
    }

  /*****************************************************************************
   * handleResume *
   * Handler for when the Resume button is pressed.
  *****************************************************************************/
  public void handleResume()
    {
    GameManager.toggleCamera(1);
    }

  /*****************************************************************************
   * handleResume *
   * Handler for when the Restart button is pressed.
  *****************************************************************************/
  public void handleRestart()
    {
    GameManager.restartGame();
    }
  }
