using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************************************************************************
 * InGameUIManager *
 * Manages the In Game UI.
*******************************************************************************/
public class InGameUIManager : MonoBehaviour
  {
  /** Score text. */
  public Text scoreText;

  /** Enemy Threats text. */
  public Text threatsText;

  /** Player score text. */
  public Text playerScoreText;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  /*****************************************************************************
   * Update *
  *****************************************************************************/
	void Update ()
    {
		
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
    }
  }
