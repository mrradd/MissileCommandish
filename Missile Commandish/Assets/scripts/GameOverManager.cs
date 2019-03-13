using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Monetization;

/*******************************************************************************
 * class GameOverManager *
 * Handles Game Over Menu functionality.
*******************************************************************************/
public class GameOverManager : MonoBehaviour
  {
  public AudioSource       audioSource;
  public Text              finalScoreText;
  public Text              reasonForLosingText;
  public VoiceSoundManager voiceSoundManager;
  public bool              playedGameOverSound;

  /** Instance of the GameManager. */
  public static GameOverManager instance;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  /*****************************************************************************
   * Start
  *****************************************************************************/
  private void Start()
    {
    if (!instance)
      instance = this;
    else
      {
      Debug.LogError("There can only be one instance of GameOverManager.");
      Destroy(gameObject);
      }

    /** HACK: For some reason I couldn't get the voice sounds prefab AudioSource
     * to be seen as active, so I am setting one here. */
    voiceSoundManager.audioSource = audioSource;

    bool citiesLost = PlayerPrefs.GetInt("NoCitiesLeft") > 0;
    //bool launchersLost = PlayerPrefs.GetInt("NoLaunchersLeft") > 0;

    /** Set final score. */
    finalScoreText.text = "Final Score\n" + PlayerPrefs.GetInt("PlayerScore").ToString();

    /** Set reason for losing text.
    if(citiesLost && launchersLost)
      reasonForLosingText.text = "All cities and launchers lost!";
    else if(citiesLost)
      reasonForLosingText.text = "All cities lost!";
    else if(launchersLost)
      reasonForLosingText.text = "All launchers lost!";
    else
      reasonForLosingText.text = "Game Over";
    */

    /** Set reason for losing text. */
    if (citiesLost)
      reasonForLosingText.text = "All cities lost!";
    else
      reasonForLosingText.text = "Game Over";
  }

  /*****************************************************************************
   * Update
  *****************************************************************************/
  private void Update()
    {
    if (Input.GetKeyDown(KeyCode.Escape))
      {
      mainMenu();
      }

    if (!playedGameOverSound)
      {
      playedGameOverSound = voiceSoundManager.playGameOver();
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
    GameObject.Find("Ad").GetComponent<UnityAdsPlacement>().ShowAd(delegate (ShowResult result)
      {
      switch(result)
        {
        case ShowResult.Finished:
          {
          Debug.Log("Ad finished.");
          restartGame();
          break;
          }
        case ShowResult.Skipped: { Debug.Log("Ad skipped."); break; }
        case ShowResult.Failed: { Debug.Log("Ad failed."); break; }
        default: { Debug.Log("No city restored for ad."); break; }
        }
      });
    }

  /*****************************************************************************
   * mainMenu *
   * Goes to the main menu.
  *****************************************************************************/
  public void mainMenu()
    {
    Debug.Log("go::mainMenu");
    SceneManager.LoadScene("MainMenuScene");
    }

  /*****************************************************************************
   * restartGame *
   * Starts a new game.
  *****************************************************************************/
  public void restartGame()
    {
    Debug.Log("go::restartGame");
    PlayerPrefs.SetInt("CameFromGameOverScreen", 1);
    SceneManager.LoadScene("MainGameScene");
    }
}
