using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class WeaponData *
 * Base weapon stats.
*******************************************************************************/
public class WeaponData : MonoBehaviour
  {
  /** Point value of the weapon. */
  public int pointsValue;

  /** Speed of the weapon. */
  public float speed;

  /** Target the weapon will move toward. */
  public Vector3 target;
  }
