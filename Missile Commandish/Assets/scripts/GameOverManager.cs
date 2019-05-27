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
  public AudioSource audioSource;
  public Text finalScoreText;
  public bool playedGameOverSound;
  public Text reasonForLosingText;
  public VoiceSoundManager voiceSoundManager;
  public Text waveText;
  public Text score1Text;
  public Text score2Text;
  public Text score3Text;
  public Text score4Text;
  public Text score5Text;
  public InputField initialsInput;


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

    /** Set final score. */
    finalScoreText.text = "Final Score\n" + PlayerPrefs.GetInt("PlayerScore").ToString();

    waveText.text = "Wave\n" + PlayerPrefs.GetInt("LastWavePlayed").ToString();

    /** Set reason for losing text. */
    reasonForLosingText.text = "All Your Base Are Belong to Us!";
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
   * mainMenu *
   * Goes to the main menu.
  *****************************************************************************/
  public void mainMenu()
    {
    Debug.Log("go::mainMenu");
    PlayerPrefs.SetInt("ContinuingGame", 0);
    SceneManager.LoadScene("MainMenuScene");
    }

  /*****************************************************************************
   * restartGame *
   * Starts a new game.
  *****************************************************************************/
  public void restartGame()
    {
    Debug.Log("go::restartGame");
    PlayerPrefs.SetInt("ContinuingGame", 0);
    SceneManager.LoadScene("MainGameScene");
    }
}
