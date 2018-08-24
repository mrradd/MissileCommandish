using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class SmartTargeter *
 * Randomly finds a live building and sets it as the target.
*******************************************************************************/
public class SmartTargeter : Targeter
  {
  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  protected override void Start()
    {
    base.Start();
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * findTarget *
   * Searches for a live building.
  *****************************************************************************/
  public override void findTarget()
    {
    List<GameObject> buildings = GameManager.activeBuildings;

    int i = Random.Range(0, buildings.Count);

    Debug.Log("SmartTarget: " + buildings[i].name);

    weaponData.target = buildings[i].transform.position;

    rotateTowardTarget();
    }
  }
