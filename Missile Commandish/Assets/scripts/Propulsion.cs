using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class Propulsion *
 * Handles movement.
*******************************************************************************/
[RequireComponent(typeof(WeaponData))]
public class Propulsion : MonoBehaviour
  {
  /** Weapon data object. */
  public WeaponData weaponData;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * Awake *
  *****************************************************************************/
  private void Awake()
    {
    if(!weaponData)
      weaponData = gameObject.GetComponent<WeaponData>();
    }

  /*****************************************************************************
   * Update *
  *****************************************************************************/
  private void Update()
    {
    if(!GameManager.instance.gamePaused)
      {
      transform.position = Vector3.MoveTowards(transform.position,
                                               weaponData.target,
                                               weaponData.speed * Time.deltaTime);      
      }
    }
  }
