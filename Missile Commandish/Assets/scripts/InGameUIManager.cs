using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************************************************************************
 * class InGameUIManager *
 * Manages the In Game UI.
*******************************************************************************/
public class InGameUIManager : MonoBehaviour
  {
  /** Player Rockets count. */
  public Text playerRocketCountText;

  /** Player score text. */
  public Text playerScoreText;

  /** Enemy Threats text. */
  public Text threatsText;

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * handlePause *
   * Handler for when the Pause button is pressed.
  *****************************************************************************/
  public void handlePause()
    {
    Debug.Log("Pause pushed.");
    GameManager.instance.pauseMenuUIManager.currentScoreText.text = "Current Score\n" + GameManager.instance.playerScore;
    GameManager.toggleCamera(2);
    }

  /*****************************************************************************
   * updatePlayerRocketCountText *
   * Updates the Player Rocket count text.
   * @param  value  Number value to display.
  *****************************************************************************/
  public void updatePlayerRocketCountText(int value)
    {
    playerRocketCountText.text = "Rockets: " + value.ToString();
    }

  /*****************************************************************************
   * updatePlayerScoreText *
   * Updates the Player score text.
   * @param  value  Number value to display.
  *****************************************************************************/
  public void updatePlayerScoreText(int value)
    {
    playerScoreText.text = value.ToString();
    }

  /*****************************************************************************
   * updateThreatCount *
   * Updates the Enemy threat count text.
   * @param  value  Number value to display.
  *****************************************************************************/
  public void updateThreatCount(int value)
    {
    threatsText.text = "Threats: " + value.ToString();
    }
  }
