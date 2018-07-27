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
      launchRocket();
      }
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * launchRocket *
   * Randomly tries to spawn an Enemy Rocket from a Random Enemy Rocket Spawn
   * Point.
  *****************************************************************************/
  protected void launchRocket()
    {
    int trigger = (int)Random.Range(0f, 125.0f);
    if(trigger == 1)
      {
      int index = (int)Random.Range(0f, GameManager.enemyRocketSpawners.Length);
      GameManager.enemyRocketSpawners[index].spawn();
      }
    }
  }
