using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class BomberTargeter *
 * Targets the farside of the screen.
*******************************************************************************/
public class BomberTargeter : Targeter
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
   * Targets the opposite side of the screen.
  *****************************************************************************/
  public override void findTarget()
    {
    weaponData.target = new Vector3(-gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }
  }
