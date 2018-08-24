using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*******************************************************************************
 * class GameOverManager *
 * Handles Game Over Menu functionality.
*******************************************************************************/
public class GameOverManager : MonoBehaviour
  {
  public Text finalScoreText;
  public Text reasonForLosingText;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  private void Start()
    {
    bool citiesLost = PlayerPrefs.GetInt("NoCitiesLeft") > 0;
    bool launchersLost = PlayerPrefs.GetInt("NoLaunchersLeft") > 0;

    /** Set final score. */
    finalScoreText.text = "Final Score\n" + PlayerPrefs.GetInt("PlayerScore").ToString();

    /** Set reason for losing text. */
    if(citiesLost)
      reasonForLosingText.text = "All cities lost!";
    else if(launchersLost)
      reasonForLosingText.text = "All launchers lost!";
    else if(citiesLost && launchersLost)
      reasonForLosingText.text = "All cities and launchers lost!";
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * restartGame *
   * Starts a new game.
  *****************************************************************************/
  public void restartGame()
    {
    Debug.Log("go::restartGame");
    SceneManager.LoadScene("MainGameScene");
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
  }
