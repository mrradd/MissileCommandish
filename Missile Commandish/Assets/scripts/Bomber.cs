using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class Bomber *
 * Flies across the screen, finds the bombs target, and when it gets to the
 * target point it drops a bomb. Handles its own destruction.
*******************************************************************************/
public class Bomber : EnemyWeapon
{
  /** Spawner for bomb launching. */ public SpawnPoint spawnPoint;
  /** Bomb target. */                public Vector3    bombTarget;

  /** Bomb dropped. */ protected bool mBombDropped;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * Start *
  *****************************************************************************/
  protected override void Start()
    {
    base.Start();
    updateWeaponCounts();
    findBombTarget();
    }

  /*****************************************************************************
   * Update *
  *****************************************************************************/
  protected override void Update()
    {
    /** Rocket reached the target. */
    if(gameObject.transform.position == weaponData.target && gameObject.activeSelf)
      {
      /** Handling destruction and active weapon count here since we do not want
        * to play explosion animation which normally handles it. */
      GameManager.updateActiveEnemyWeaponCount(-1);
      weaponDestroyer.destroy();
      }

    if((int)gameObject.transform.position.x == (int)bombTarget.x && !mBombDropped)
      dropBomb();
    }

  /*****************************************************************************
   * Methods *
  *****************************************************************************/
  /*****************************************************************************
   * dropBomb *
   * Spawns a bomb.
  *****************************************************************************/
  public void dropBomb()
    {
    mBombDropped = true;
    GameObject obj = spawnPoint.spawn();
    obj.GetComponent<WeaponData>().target = bombTarget;
    obj.GetComponent<BombTargeter>().rotateTowardTarget();

    /** Lower the bomb a bit so it doesn't overlap the bomber asset. */
    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 2.0f, 0);
    }

  /*****************************************************************************
   * findBombTarget *
   * Targets the opposite side of the screen.
  *****************************************************************************/
  public void findBombTarget()
    {
    List<GameObject> bldgs = GameManager.activeBuildings;
    int i = (int)Random.Range(0, bldgs.Count);
    bombTarget = bldgs[i].transform.position;
    }

  /*****************************************************************************
   * updateWeaponCounts *
   * Updates the weapon counts.
  *****************************************************************************/
  protected override void updateWeaponCounts()
    {
    GameManager.updateActiveEnemyWeaponCount(1);
    GameManager.updateEnemyWeaponCount(-1);
    }
  }
