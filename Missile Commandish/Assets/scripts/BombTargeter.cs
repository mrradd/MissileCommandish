using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class BombTargeter *
 * Only used for allowing access to rotating the bomb. Bomber handles targeting.
*******************************************************************************/
public class BombTargeter : Targeter
  {
  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  protected override void Start()
    {
    if(!weaponData)
      weaponData = gameObject.GetComponent<WeaponData>();
    }
  }
