using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;

/*******************************************************************************
 * class LevelClearedUIManager *
 * Handles Level Cleared menu functionality.
*******************************************************************************/
public class LevelClearedUIManager : MonoBehaviour
  {
  /** Button to display ad and restore city. */
  public Button restoreCityButton;

  /** Bonus from cities. */
  public Text cityBonusText;

  /** Notifies city restored. */
  public Text cityRestoredText;

  /** Bonus from launchers. */
  public Text launcherBonusText;

  /** Bonus from rockets. */
  public Text rocketBonusText;

  /** Player's current score. */
  public Text totalScoreText;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  protected void Update()
    {
    if (Input.GetKeyDown(KeyCode.Escape))
      {
      handleContinue();
      }
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * displayAd *
   * Displays an ad and tries to reward the player.
  *****************************************************************************/
  public void displayAd()
    {
    if(!GameManager.instance.cityRestored && GameManager.activeCityCount < GameManager.instance.cities.Length)
      {
      GameObject.Find("Ad").GetComponent<UnityAdsPlacement>().ShowAd(delegate (ShowResult result)
        {
        switch(result)
          {
          case ShowResult.Finished:
            {
            Debug.Log("Ad finished.");
            GameManager.restoreCity();
            cityRestoredText.gameObject.SetActive(GameManager.instance.cityRestored);
            restoreCityButton.gameObject.SetActive(false);
            break;
            }
          case ShowResult.Skipped: { Debug.Log("Ad skipped."); break; }
          case ShowResult.Failed: { Debug.Log("Ad failed."); break; }
          default: { Debug.Log("No city restored for ad."); break; }
          }
        });      
      }
    }

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
    restoreCityButton.gameObject.SetActive(!GameManager.instance.cityRestored && GameManager.activeCityCount < GameManager.instance.cities.Length);

    int cityBonus     = GameManager.instance.cityBonus     * GameManager.activeCityCount;
    int launcherBonus = GameManager.instance.launcherBonus * GameManager.activeLauncherCount;
    int rocketBonus   = GameManager.instance.rocketBonus   * GameManager.instance.playerRocketCounter;

    GameManager.updatePlayerScore(cityBonus + launcherBonus + rocketBonus);

    cityBonusText.text     = GameManager.instance.cityBonus     + " x " + GameManager.activeCityCount              + " = " + cityBonus;
    launcherBonusText.text = GameManager.instance.launcherBonus + " x " + GameManager.activeLauncherCount          + " = " + launcherBonus;
    rocketBonusText.text   = GameManager.instance.rocketBonus   + " x " + GameManager.instance.playerRocketCounter + " = " + rocketBonus;

    cityRestoredText.gameObject.SetActive(false);

    totalScoreText.text = "Total Score: " + GameManager.instance.playerScore;
    }
  }
