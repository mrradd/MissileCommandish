using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class Weapon *
 * Base class for all Weapons.
*******************************************************************************/
[RequireComponent(typeof(WeaponData), typeof(WeaponDestroyer))]
public class Weapon : MonoBehaviour
  {
  /** Explosion animation to instantiate on death. */
  public GameObject explosion;

  /** Weapon data object. */
  public WeaponData weaponData;

  /** Weapon destroyer. */
  public WeaponDestroyer weaponDestroyer;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * Start *
  *****************************************************************************/
	protected virtual void Start ()
    {
    if(!weaponData)
      weaponData = gameObject.GetComponent<WeaponData>();

    if(!weaponDestroyer)
      weaponDestroyer = gameObject.GetComponent<WeaponDestroyer>();
	  }

  /*****************************************************************************
   * OnTriggerEnter2D *
  *****************************************************************************/
  protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    playExplosionAnimation();
    }

  /*****************************************************************************
   * Update *
  *****************************************************************************/
  protected virtual void Update()
    {
    /** Rocket reached the target. */
    if(gameObject.transform.position == weaponData.target && gameObject.activeSelf)
      {
      weaponDestroyer.destroy();

      if(!gameObject.activeSelf)
        playExplosionAnimation();
      }
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  protected void playExplosionAnimation()
    {
    /** Instantiate Explosion animation object. */
    Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }
  }
