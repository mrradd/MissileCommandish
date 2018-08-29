using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class Targeter *
 * Sets the weapon's target.
*******************************************************************************/
[RequireComponent(typeof(WeaponData))]
public class Targeter : MonoBehaviour
  {
  /** Weapon data object. */
  public WeaponData weaponData;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  protected virtual void Start()
    {
    if(!weaponData)
      weaponData = gameObject.GetComponent<WeaponData>();

    findTarget();
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * findTarget *
   * Searches for a target. This instance finds a random target and rotates
   * toward it.
  *****************************************************************************/
  public virtual void findTarget()
    {
    weaponData.target = new Vector2(Random.Range(GameManager.boundaryLeftX,
                                                 GameManager.boundaryRightX),
                                                 GameManager.floorY);
    rotateTowardTarget();
    }

  /*****************************************************************************
   * rotateTowardTarget *
   * Rotates the weapon toward its target.
  *****************************************************************************/
  public void rotateTowardTarget()
    {
    Vector2 localPosition = gameObject.transform.localPosition;
    Vector2 offset        = new Vector2(weaponData.target.x - localPosition.x,
                                        weaponData.target.y - localPosition.y);
    
    float angleDeg = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

    gameObject.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
    }
  }
