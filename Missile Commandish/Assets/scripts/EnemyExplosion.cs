using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class EnemyExplosion *
 * Handles Enemy Explosion functionality.
*******************************************************************************/
public class EnemyExplosion : Explosion
  {
  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * Update *
  *****************************************************************************/
  protected override void Update()
    {
    /** Destroy the Explosion. */
    if(animator.GetCurrentAnimatorStateInfo(0).IsName("ExplosionEnd"))
      {
      GameManager.updateActiveEnemyWeaponCount(-1);
      Debug.Log("Active: " + GameManager.instance.activeEnemyWeapons);
      Destroy(gameObject);
      }
    }
  }
