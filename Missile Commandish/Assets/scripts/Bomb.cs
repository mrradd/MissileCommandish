using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class Bomb *
 * Drops from a bomber onto a target.
*******************************************************************************/
public class Bomb : EnemyWeapon
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
