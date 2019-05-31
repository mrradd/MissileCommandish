using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/*******************************************************************************
 * class HighScoreManager *
 * Handles High Score screen functionality.
*******************************************************************************/
public class HighScoresManager : MonoBehaviour
  {
  public Text[] highScores;

  protected HighScoreList mHighScoreList;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  /*****************************************************************************
   * Start
  *****************************************************************************/
  protected void Start()
    {
    initHighScoreList();
    }

  /*****************************************************************************
   * Update
  *****************************************************************************/
  protected void Update()
    {
    if(Input.GetKeyDown(KeyCode.Escape))
      {
      SceneManager.LoadScene("MainMenuScene");
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
    mHighScoreList = mHighScoreList ?? new HighScoreList();
    for(int i = 0; i < mHighScoreList.highScores.Count; i++)
      {
      HighScoreEntry hse = mHighScoreList.highScores[i];

      highScores[i].text = getPlaceText(hse.place) + " " + hse.name + " - " + hse.score;
      }
    }

  /*****************************************************************************
   * playGame *
   * Starts a new game.
  *****************************************************************************/
  public void mainMenu()
    {
    Debug.Log("mainMenu");
    SceneManager.LoadScene("MainMenuScene");
    }
  }
