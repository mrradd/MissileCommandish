using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class AIController *
 * Controls non Player objects.
*******************************************************************************/
public class AIController : MonoBehaviour
  {
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
    if(trigger == 1)
      {
      trigger = (int)Random.Range(0f, 100.0f);

      ///** Launch Enemy Rocket. */
      //if(trigger >= 1 && trigger <= 66)
      //  {
      //  int index = (int)Random.Range(0f, GameManager.enemyRocketSpawners.Length);
      //  GameManager.enemyRocketSpawners[index].spawn();        
      //  }

      ///** Launch MIRV. */
      //else if(trigger >= 67 && trigger <= 100)
        //{
        //int index = (int)Random.Range(0f, GameManager.mirvRocketSpawners.Length);
        //GameManager.mirvRocketSpawners[index].spawn();
        //}
      int index = (int)Random.Range(0f, GameManager.mirvRocketSpawners.Length);
      GameManager.mirvRocketSpawners[index].spawn();
      }
    }
  }
