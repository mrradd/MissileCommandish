using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class Building *
 * Base class for a Building.
*******************************************************************************/
public class Building : MonoBehaviour
  {
  /** Amount of time to shake the camera for. */
  public float cameraShakeTime;

  /** Destroyed version of the building. This is set later. */
  public GameObject destroyedVersion;

  /** Explosion animation to instantiate on death. */
  public GameObject explosion;

  /** Flaming animation. */
  public GameObject flamingAnimation;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * OnTriggerEnter2D *
  *****************************************************************************/
  protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    initDestroyedVesion(true);
    }

  /*****************************************************************************
   * Methods *
  *****************************************************************************/
  /*****************************************************************************
   * initDestroyedVesion *
   * Initializes the destroyed version of the building.
  *****************************************************************************/
  public void initDestroyedVesion(bool playExplosion)
    {
    /** Adjust the position of the flaming building object. */
    Vector3 pos = gameObject.transform.position;

    /** Instantiate explosion animation object. */
    if(playExplosion)
      {
      Instantiate(explosion, pos, Quaternion.identity);
      GameManager.getMainCameraShaker().incrementShakeTimer(cameraShakeTime);
      }
     
    pos.y += 2.5f;

    /** Instantiate flaming building animation object. */
    destroyedVersion = Instantiate(flamingAnimation, pos, Quaternion.identity);

    /** Set the name of the flaming building for future reference. */
    destroyedVersion.name = gameObject.name + "-Flames";

    /** Deactivate building. */
    gameObject.SetActive(false);    
    }
  }
