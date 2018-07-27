using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class Building *
 * Base class for a Building.
*******************************************************************************/
public class Building : MonoBehaviour
  {
  /** Flaming animation. */
  public GameObject flamingAnimation;

  /*****************************************************************************
   * OnTriggerEnter2D *
  *****************************************************************************/
  protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    /** Adjust the position of the flaming building object. */
    Vector3 pos = gameObject.transform.position;
    pos.y += 2.5f;

    /** Instantiate flaming building animation object. */
    GameObject obj = Instantiate(flamingAnimation, pos, Quaternion.identity);

    /** Set the name of the flaming building for future reference. */
    obj.name = gameObject.name + "-Flames";

    /** Deactivate building. */
    gameObject.SetActive(false);
    }
  }
