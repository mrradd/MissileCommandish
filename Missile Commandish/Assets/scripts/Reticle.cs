using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class Reticle *
 * Does things to the reticle.
*******************************************************************************/
public class Reticle : MonoBehaviour
  {
  public int speed = 10;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  void Update()
    {
    StartCoroutine(rotateForSeconds());
    }

  /*****************************************************************************
   * Methods *
  *****************************************************************************/
  /*****************************************************************************
   * rotateForSeconds *
   * Rotates the reticle for a certain amount of time.
  *****************************************************************************/
  IEnumerator rotateForSeconds()
    {
    float time = 5;     //How long will the object be rotated?

    while (time > 0)     //While the time is more than zero...
      {
      transform.Rotate(0, 0, Time.deltaTime * speed);

      time -= Time.deltaTime;     //Decrease the time- value one unit per second.

      yield return null;     //Loop the method.
      }
    }
}
