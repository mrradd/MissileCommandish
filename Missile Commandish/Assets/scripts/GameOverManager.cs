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
  public Text newHighScoreText;
  public bool playedGameOverSound;
  public Text reasonForLosingText;
  public VoiceSoundManager voiceSoundManager;
  public Text waveText;

  /** High score stuff. */
  public InputField initialsInput;
  public Text[] scoresText;

  /** Instance of the GameManager. */
  public static GameOverManager instance;

  protected HighScoreList mHighScoreList = null;
  protected HighScoreEntry mNewHighScore;

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
    instance.voiceSoundManager.audioSource = instance.audioSource;

    /** Set final score. */
    instance.finalScoreText.text = "Final Score\n" + PlayerPrefs.GetInt("PlayerScore").ToString();

    //waveText.text = "Wave\n" + PlayerPrefs.GetInt("LastWavePlayed").ToString();

    /** Set reason for losing text. */
    instance.reasonForLosingText.text = "All Your Base Are Belong to Us!";

    initHighScoreList();

    foreach(HighScoreEntry hse in instance.mHighScoreList.highScores)
      {
      /** Create a new high score object and activate initials input and high score text prompt. */
      if(hse.score < PlayerPrefs.GetInt("PlayerScore"))
        {
        instance.mNewHighScore = new HighScoreEntry("", PlayerPrefs.GetInt("PlayerScore"), hse.place);
        instance.newHighScoreText.gameObject.SetActive(true);
        instance.initialsInput.gameObject.SetActive(true);
        break;
        }
      else
        {
        instance.newHighScoreText.gameObject.SetActive(false);
        instance.initialsInput.gameObject.SetActive(false);
        }       
      }
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

    if (!instance.playedGameOverSound)
      {
      instance.playedGameOverSound = instance.voiceSoundManager.playGameOver();
      }
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * getPlaceText *
   * Gets the text for the passed in integer up to 5.
   * @param  place  Place to get text version of.
  *****************************************************************************/
  protected string getPlaceText(int place)
    {
    return place == 1 ? "1st" :
           place == 2 ? "2nd" :
           place == 3 ? "3rd" :
           place == 4 ? "4th" : "5th";
    }

  /*****************************************************************************
   * initHighScoreList *
   * Initializes the score text list.
  *****************************************************************************/
  protected void initHighScoreList()
    {
    /** Init high score stuff. */
    instance.mHighScoreList = instance.mHighScoreList ?? new HighScoreList();
    for(int i = 0; i < instance.mHighScoreList.highScores.Count; i++)
      {
      HighScoreEntry hse = instance.mHighScoreList.highScores[i];

      instance.scoresText[i].text = getPlaceText(hse.place) + " " + hse.name + " - " + hse.score;
      }
    }

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

  /*****************************************************************************
   * updateNewHighScore *
   * Updates the new high score entry with a name, sorts the high scores, and 
   * updates the score text list.
  *****************************************************************************/
  public void updateNewHighScore(string textInput)
    {
    instance.mNewHighScore.name = textInput.ToUpper();
    for(int i = 0; i < instance.mHighScoreList.highScores.Count; i++)
      {
      if(instance.mHighScoreList.highScores[i].place == instance.mNewHighScore.place)
        {
        HighScoreList tempHSL = instance.mHighScoreList;
        tempHSL.highScores.Add(instance.mNewHighScore);

        /** Bubble sort. */
        int x, j;
        HighScoreEntry tempHSE;
        bool swapped;
        for (x = 0; i < tempHSL.highScores.Count - 1; x++)
          { 
          swapped = false;
          for (j = 0; j < tempHSL.highScores.Count - x - 1; j++)
            {
            if (tempHSL.highScores[j].score < tempHSL.highScores[j + 1].score)
              {
              tempHSE = tempHSL.highScores[j];
              tempHSL.highScores[j] = tempHSL.highScores[j + 1];
              tempHSL.highScores[j + 1] = tempHSE;
              swapped = true;
              }
            }

          if (swapped == false)
            {
            break;
            }
          }

        /** Remove the last element since we only want 5 scores. */
        tempHSL.highScores.RemoveAt(5);
        instance.mHighScoreList = tempHSL;

        /** Update player prefs. */
        int newPlace = 1;
        foreach(HighScoreEntry hse in instance.mHighScoreList.highScores)
          {
          hse.place = newPlace;
          PlayerPrefs.SetString("HS" + hse.place + "-Name", hse.name);
          PlayerPrefs.SetInt("HS" + hse.place + "-Score", hse.score);
          PlayerPrefs.SetInt("HS" + hse.place + "-Place", hse.place);
          newPlace++;
          }

        PlayerPrefs.Save();

        instance.newHighScoreText.gameObject.SetActive(false);
        instance.initialsInput.gameObject.SetActive(false);

        initHighScoreList();
        break;
        }
      }
    }
  }
