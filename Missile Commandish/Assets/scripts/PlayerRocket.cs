using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class PlayerRocket *
 * Handles Player Rocket functionality.
*******************************************************************************/
public class PlayerRocket : Rocket
  {
  public GameObject reticle;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * Start *
  *****************************************************************************/
  protected override void Start()
    {
    base.Start();
    updateWeaponCounts();
    }

  /*****************************************************************************
   * Start *
  *****************************************************************************/
  protected override void Update()
    {
    base.Update();
    reticle.transform.position = weaponData.target;
    }

  /*****************************************************************************
   * OnCollisionEnter2D *
  *****************************************************************************/
  protected override void OnTriggerEnter2D(Collider2D collision)
    {
    base.OnTriggerEnter2D(collision);
    }

  /*****************************************************************************
   * Methods 
  *****************************************************************************/
  /*****************************************************************************
   * playExplosionAnimation *
   * Plays the explosion animation.
  *****************************************************************************/
  protected override void playExplosionAnimation()
    {
    Destroy(reticle);

    /** Instantiate Explosion animation object. */
    Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }

  /*****************************************************************************
   * updateWeaponCounts *
   * Updates the weapon counts.
  *****************************************************************************/
  protected override void updateWeaponCounts()
    {
    GameManager.updatePlayerRocketCount(-1);
    }
  }
