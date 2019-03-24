using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class CameraShaker *
 * Shakes the camera around.
*******************************************************************************/
public class CameraShaker : MonoBehaviour
  {
  public Camera cameraTarget;
  public float shakeAmount = 0.75f;

  protected Vector3 mCameraOriginalPosition;
  protected float mShakeTimer = 0.0f;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  public void Awake()
    {
    mCameraOriginalPosition = cameraTarget.transform.localPosition;
    }

  public void Update()
    {
    shake();
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * decrementShakeTimer *
   * Decreases the time left for shaking.
  *****************************************************************************/
  public void decrementShakeTimer()
    {
    mShakeTimer -= Time.deltaTime;
    mShakeTimer = mShakeTimer < 0.0f ? 0.0f : mShakeTimer;
    }

  /*****************************************************************************
   * incrementShakeTimer *
   * Increases the time left for shaking by the given value.
   * 
   * @param  seconds  Amount of time to increase the timer by.
  *****************************************************************************/
  public void incrementShakeTimer(float seconds) { mShakeTimer += seconds; }

  /*****************************************************************************
   * shake *
   * Shakes the randomly camera.
  *****************************************************************************/
  public void shake()
    {
    /** Shake shake shake. */
    if(mShakeTimer > 0.0f)
      {
      Vector3 vec3 = Random.insideUnitCircle * shakeAmount;

      /** Adjust for camera z. */
      vec3.z = -10.0f;

      cameraTarget.transform.localPosition = vec3;

      decrementShakeTimer();
      }

    /** Reset camera after shaking. */
    else if(cameraTarget && cameraTarget.transform.localPosition != mCameraOriginalPosition)
      cameraTarget.transform.localPosition = mCameraOriginalPosition;
    }

  /*****************************************************************************
   * zeroOutShakeTimer *
   * Sets shake timer to zero.
  *****************************************************************************/
  public void zeroOutShakeTimer()
    {
    mShakeTimer = 0;
    }
  }
