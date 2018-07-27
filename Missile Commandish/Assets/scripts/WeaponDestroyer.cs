using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class WeaponDestroyer *
 * Destroys the weapon.
*******************************************************************************/
[RequireComponent(typeof(WeaponData))]
public class WeaponDestroyer : MonoBehaviour
  {
  /** Weapon data object. */
  public WeaponData weaponData;

  /** Time limit. */
  public float timeLimit;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * Awake *
  *****************************************************************************/
  void Awake()
    {
    if(!weaponData)
      weaponData = gameObject.GetComponent<WeaponData>();
    }

  /*****************************************************************************
   * OnCollisionEnter2D *
  *****************************************************************************/
  void OnTriggerEnter2D(Collider2D collision)
    {
    destroy();
    }

  /*****************************************************************************
   * Methods *
  *****************************************************************************/
  /*****************************************************************************
   * destroy *
   * Destroys the object.
  *****************************************************************************/
  public void destroy()
    {
    gameObject.SetActive(false);
    Destroy(gameObject, timeLimit);
    }
  }