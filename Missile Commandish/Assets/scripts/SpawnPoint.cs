using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class SpawnPoint *
 * Spawns an object
*******************************************************************************/
public class SpawnPoint : MonoBehaviour
  {
  /** Game object to spawn. */
  public GameObject obj;
  //TODO CH  FINISH MIRV
  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * spawn *
   * Spawns an object at this position.
  *****************************************************************************/
  public GameObject spawn()
    {
    return Instantiate(obj, gameObject.transform.position, Quaternion.identity);
    }
  }
