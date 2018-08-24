using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class EnemyRocket *
 * Handles Enemy Rocket functionality.
*******************************************************************************/
public class EnemyRocket : Rocket
  {
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
   * OnCollisionEnter2D *
  *****************************************************************************/
  protected override void OnTriggerEnter2D(Collider2D collision)
    {
    base.OnTriggerEnter2D(collision);

    /** Check if collided into a Player Rocket. */
    if(collision.gameObject.tag == GameManager.PLAYER_ROCKET_TAG)
      GameManager.updatePlayerScore(weaponData.pointsValue * 2);

    /** Check if collided into a Player Rocket Explosion. */
    else if(collision.gameObject.tag == GameManager.PLAYER_EXPLOSION_TAG)
      GameManager.updatePlayerScore(weaponData.pointsValue);

    /** Check if collided into an Enemy Explosion. */
    else if(collision.gameObject.tag == GameManager.ENEMY_EXPLOSION_TAG)
      GameManager.updatePlayerScore(weaponData.pointsValue / 2);
    }

  /*****************************************************************************
   * Methods 
  *****************************************************************************/
  /*****************************************************************************
   * updateWeaponCounts *
   * Updates the weapon counts.
  *****************************************************************************/
  protected override void updateWeaponCounts()
    {
    GameManager.updateActiveEnemyWeaponCount(1);
    GameManager.updateEnemyWeaponCount(-1);
    }
  }
