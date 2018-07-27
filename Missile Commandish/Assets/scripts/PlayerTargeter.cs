using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class PlayerTargeter *
 * Sets the Weapon's Target.
*******************************************************************************/
public class PlayerTargeter : Targeter
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
   * Sets target based on player click.
  *****************************************************************************/
  public override void findTarget()
    {
    Vector3 t         = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    weaponData.target = new Vector3(t.x, t.y, 0);
    rotateTowardTarget();
    }
  }
