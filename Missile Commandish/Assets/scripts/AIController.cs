using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class AIController *
 * Controls non Player objects.
*******************************************************************************/
public class AIController : MonoBehaviour
  {
  public int mirvAmmo;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  /*****************************************************************************
   * Update *
  *****************************************************************************/
  private void Update()
    {
    if(!GameManager.instance.gamePaused && GameManager.instance.enemyWeaponCounter > 0)
      {
      spawnEnemyWeapon();
      }
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * spawnEnemyWeapon *
   * Randomly tries to spawn an Enemy Weapon from a random Enemy Spawn Point.
  *****************************************************************************/
  protected void spawnEnemyWeapon()
    {
    int trigger = (int)Random.Range(0f, 125.0f);
    if(trigger == 1 || GameManager.instance.gameLost || GameManager.instance.playerRocketCounter <= 0)
      {
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
      else if(trigger >= 90 && trigger <= 100 && GameManager.instance.currentWave >= 5 && GameManager.instance.mirvCount > 0)
        {
        int index = (int)Random.Range(0f, GameManager.mirvRocketSpawners.Length);
        GameManager.mirvRocketSpawners[index].spawn();
        GameManager.instance.mirvCount--;
        }
        //int index = (int)Random.Range(0f, GameManager.enemyRocketSpawners.Length);
        //GameManager.enemyRocketSpawners[index].spawn();
      }
    }
  }
