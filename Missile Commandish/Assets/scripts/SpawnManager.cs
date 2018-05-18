using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * SpawnManager *
 * Spawning of the weapon.
*******************************************************************************/
public class SpawnManager : MonoBehaviour
  {
  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  /*****************************************************************************
   * Start *
  *****************************************************************************/
  private void Start()
    {

    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * spawn *
   * Sets the initial position of the weapon based on a random spawn point
   * pertaining to its tag.
  *****************************************************************************/
  public void spawn()
    {
    if(gameObject.tag == "EnemyRocket")
      {
      GameObject[] list = GameManager.instance.enemyRocketSpawnPoints;
      gameObject.transform.position = list[Random.Range(0, list.Length)].transform.position;
      }
    }
  }
