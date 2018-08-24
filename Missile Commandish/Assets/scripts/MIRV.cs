using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class MIRV *
 * Handles MIRV functionality. A MIRV launches multiple smaller rockets, and
 * targets active cities and launchers.
*******************************************************************************/
public class MIRV : EnemyRocket
  {
  /** Spawner for missile launching. */ public SpawnPoint spawnPoint;
  /** Number of missiles to spawn. */   public int        numberOfBabies;

  /** Babies launched flag. */ protected bool hadBabies;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * Update *
  *****************************************************************************/
  protected override void Update()
    {
    base.Update();
    launchBabies();
    }

  /*****************************************************************************
   * Methods 
  *****************************************************************************/
  /*****************************************************************************
   * launchBabies *
   * Launches multiple smaller missiles.
  *****************************************************************************/
  protected void launchBabies()
    {
    float triggerY = (int)Random.Range(GameManager.playAreaTopY, GameManager.mirvThresholdY);

    if(!hadBabies && transform.position.y <= triggerY)
      {
      hadBabies = true;
      for(int i = 0; i < numberOfBabies; i++)
        spawnPoint.spawn();
      }
    }
  }
