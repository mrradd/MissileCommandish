using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class Bomb *
 * Drops from a bomber onto a target.
*******************************************************************************/
public class Bomb : Weapon
  {

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
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
   * Start *
  *****************************************************************************/
  protected override void Start()
    {
    base.Start();
    updateWeaponCounts();
    }

  /*****************************************************************************
   * Update *
  *****************************************************************************/
  protected override void Update()
    {
    /** Rocket reached the target. */
    if(gameObject.transform.position == weaponData.target && gameObject.activeSelf)
      {
      /** Handling destruction and active weapon count here since we do not want
        * to play explosion animation which normally handles it. */
      GameManager.updateActiveEnemyWeaponCount(-1);
      weaponDestroyer.destroy();
      }
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
    }
  }
