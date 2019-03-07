using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/*******************************************************************************
 * class MainMenuManager *
 * Handles Main Menu functionality.
*******************************************************************************/
public class MainMenuManager : MonoBehaviour
  {
  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  protected void Start()
    {
    Debug.Log(GetUniqueID());
    }
  protected void Update()
    {
    if(Input.GetKeyDown(KeyCode.Escape))
      {
      quit();
      }
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * playGame *
   * Starts a new game.
  *****************************************************************************/
  public void playGame()
    {
    Debug.Log("playGame");
    SceneManager.LoadScene("MainGameScene");
    }

  /*****************************************************************************
   * quit *
   * Quits the game.
  *****************************************************************************/
  public void quit()
    {
    Debug.Log("quit");
    PlayerPrefs.SetInt("CameFromGameOverScreen", 0);
    Application.Quit();
    }

  public static string GetUniqueID()
  {
    string key = "ID";

    var random = new System.Random();
    DateTime epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
    double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;

    string uniqueID = Application.systemLanguage                            //Language
            + "-" + Application.platform                                            //Device    
            + "-" + String.Format("{0:X}", Convert.ToInt32(timestamp))                //Time
            + "-" + String.Format("{0:X}", Convert.ToInt32(Time.time * 1000000))        //Time in game
            + "-" + String.Format("{0:X}", random.Next(1000000000));                //random number

    Debug.Log("Generated Unique ID: " + uniqueID);

    if (PlayerPrefs.HasKey(key))
    {
      uniqueID = PlayerPrefs.GetString(key);
    }
    else
    {
      PlayerPrefs.SetString(key, uniqueID);
      PlayerPrefs.Save();
    }

    return uniqueID;
  }
}
