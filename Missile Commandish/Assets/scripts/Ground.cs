using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class Ground *
 * It's the ground...
*******************************************************************************/
public class Ground : MonoBehaviour
  {
  /** Amount of time to shake the camera for. */
  public float cameraShakeTime;

  /*****************************************************************************
   * OnTriggerEnter2D *
  *****************************************************************************/
  protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    GameManager.getMainCameraShaker().incrementShakeTimer(cameraShakeTime);
    }
  }
