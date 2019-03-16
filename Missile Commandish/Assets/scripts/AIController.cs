using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class AIController *
 * Controls non Player objects.
*******************************************************************************/
public class AIController : MonoBehaviour
  {
  public    float timeBetweenLaunches = 3f;
  protected float mTBLCounter         = 0f;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  /*****************************************************************************
   * Update *
  *****************************************************************************/
  private void Update()
    {
    if(!GameManager.instance.gamePaused)
      {
      spawnEnemyWeapon();
      }
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * spawnEnemyWeapon *
   * Randomly tries to spawn a random number of Enemy Weapons from a random Enemy
   * Spawn Point. Spawn weapon if trigger is hit, time between launches is met.
   * To end a level faster it will also launch when game lost, or if the player
   * runs out of rockets (the apocalypse effect: all remaining weapons dumped).
  *****************************************************************************/
  protected void spawnEnemyWeapon()
    {
    int trigger = (int)Random.Range(0f, 125.0f);

    mTBLCounter += Time.deltaTime;

    if(trigger == 1 || mTBLCounter >= timeBetweenLaunches || GameManager.instance.gameLost || GameManager.activeLauncherCount <= 0 ||
       GameManager.instance.playerRocketCounter <= 0)
      {
      mTBLCounter = 0f;

      /** Launch a random number of weapons. */
      float qtyToLaunch = Mathf.Floor((GameManager.instance.currentWave / 3));
      int launchCount = 1;//(int)Random.Range(1f, qtyToLaunch > 3f ? 3f : qtyToLaunch);
      for(int i = 0; i < launchCount; i++)
        {
        /** Make sure there is enough ammo. */
        if(GameManager.instance.enemyWeaponCounter <= 0)
          return;
        
        trigger = (int)Random.Range(1f, 100.0f);

        /** Launch Enemy Rocket. */
        if(trigger >= 1 && trigger <= 66)
          {
          int index = (int)Random.Range(0f, GameManager.enemyRocketSpawners.Length);
          GameManager.enemyRocketSpawners[index].spawn();
          }

        /** Launch bomber. */
        else if(trigger >= 67 && trigger <= 89 && GameManager.instance.currentWave >= 3 && GameManager.instance.bomberCount > 0)
          {
          int index = (int)Random.Range(0f, GameManager.bomberSpawners.Length);
          GameManager.bomberSpawners[index].spawn();
          GameManager.instance.bomberCount--;
          }

        /** Launch MIRV. */
        else if(trigger >= 90 && trigger <= 100 && GameManager.instance.currentWave >= 6 && GameManager.instance.mirvCount > 0)
          {
          int index = (int)Random.Range(0f, GameManager.mirvRocketSpawners.Length);
          GameManager.mirvRocketSpawners[index].spawn();
          GameManager.instance.mirvCount--;
          }
        }
      }
    }
  }
