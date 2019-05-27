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

  /** Current wave text. */
  public Text currentWaveText;

  /** Current high score text. */
  public Text highScoreText;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  protected void Update()
    {
    if(Input.GetKeyDown(KeyCode.Escape))
      {
      handlePause();
      }
    }

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
    GameManager.instance.pauseMenuUIManager.updateText();
    GameManager.toggleCamera(2);
    }

  /*****************************************************************************
   * updateCurrentWaveText *
   * Updates the current wave text.
  *****************************************************************************/
  public void updateCurrentWaveText()
    {
    currentWaveText.text = "Wave: " + GameManager.instance.currentWave;
    }

  /*****************************************************************************
   * updatePlayerRocketCountText *
   * Updates the Player Rocket count text.
   * @param  val  Value to print for qty of rockets.
  *****************************************************************************/
  public void updatePlayerRocketCountText(int val)
    {
    playerRocketCountText.text = "Missiles: " + val;
    }

  /*****************************************************************************
   * updatePlayerScoreText *
   * Updates the Player score text.
  *****************************************************************************/
  public void updatePlayerScoreText()
    {
    playerScoreText.text = GameManager.instance.playerScore.ToString();
    }
  }
