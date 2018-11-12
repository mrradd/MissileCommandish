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
  /** Bonus from cities. */
  public Text cityBonusText;

  /** Notifies city restored. */
  public Text cityRestoredText;

  /** Bonus from launchers. */
  public Text launcherBonusText;

  /** Notifies launcher restored. */
  public Text launcherRestoredText;

  /** Bonus from rockets. */
  public Text rocketBonusText;

  /** Player's current score. */
  public Text totalScoreText;

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * handleQuit *
   * Handler for when the Quit button is pressed.
  *****************************************************************************/
  public void handleQuit()
    {
    Debug.Log("lc::handleQuit");
    GameManager.mainMenu();
    }

  /*****************************************************************************
   * handleContinue *
   * Handler for when the Continue button is pressed.
  *****************************************************************************/
  public void handleContinue()
    {
    Debug.Log("lc::handleContinue");
    GameManager.startNextLevel();
    }

  /*****************************************************************************
   * handleRestart *
   * Handler for when the Restart button is pressed.
  *****************************************************************************/
  public void handleRestart()
    {
    Debug.Log("lc::handleRestart");
    GameManager.restartGame();
    }

  /*****************************************************************************
   * updateText *
   * Updates the UI text.
  *****************************************************************************/
  public void updateText()
    {
    int cityBonus     = GameManager.instance.cityBonus     * GameManager.activeCityCount;
    int launcherBonus = GameManager.instance.launcherBonus * GameManager.activeLauncherCount;
    int rocketBonus   = GameManager.instance.rocketBonus   * GameManager.instance.playerRocketCounter;

    GameManager.updatePlayerScore(cityBonus + launcherBonus + rocketBonus);

    cityBonusText.text     = GameManager.instance.cityBonus     + " x " + GameManager.activeCityCount              + " = " + cityBonus;
    launcherBonusText.text = GameManager.instance.launcherBonus + " x " + GameManager.activeLauncherCount          + " = " + launcherBonus;
    rocketBonusText.text   = GameManager.instance.rocketBonus   + " x " + GameManager.instance.playerRocketCounter + " = " + rocketBonus;

    cityRestoredText.gameObject.SetActive(GameManager.instance.cityRestored);
    launcherRestoredText.gameObject.SetActive(GameManager.instance.launcherRestored);

    totalScoreText.text = "Total Score: " + GameManager.instance.playerScore;
    }
  }
