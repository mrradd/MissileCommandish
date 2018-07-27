using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class Explosion *
 * Base class for an Explosion.
*******************************************************************************/
[RequireComponent(typeof(Animator))]
public class Explosion : MonoBehaviour
  {
  public Animator animator;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * Update *
  *****************************************************************************/
  protected virtual void Update()
    {
    /** Destroy the Explosion. */
    if(animator.GetCurrentAnimatorStateInfo(0).IsName("ExplosionEnd"))
      {
      Destroy(gameObject);
      }
    }
  }
