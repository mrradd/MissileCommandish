using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************************************************************************
 * class EnemyWeapon *
 * Base class for Enemy Weapons.
*******************************************************************************/
public class EnemyWeapon : Weapon
  {
  public Text textToast;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * OnCollisionEnter2D *
  *****************************************************************************/
  protected override void OnTriggerEnter2D(Collider2D collision)
    {
    base.OnTriggerEnter2D(collision);

    /** Check if collided into a Player Rocket. */
    if (collision.gameObject.tag == GameManager.PLAYER_ROCKET_TAG)
      spawnPointsValueText(weaponData.pointsValue * 2);

    /** Check if collided into a Player Rocket Explosion. */
    else if (collision.gameObject.tag == GameManager.PLAYER_EXPLOSION_TAG)
      spawnPointsValueText(weaponData.pointsValue);

    /** Check if collided into an Enemy Explosion. */
    else if (collision.gameObject.tag == GameManager.ENEMY_EXPLOSION_TAG)
      spawnPointsValueText(weaponData.pointsValue / 2);
    }

  /*****************************************************************************
   * Methods 
  *****************************************************************************/
  /*****************************************************************************
   * spawnPointsValueText *
   * Spawns a Text object with the points value as its label.
  *****************************************************************************/
  public void spawnPointsValueText(int pointsValue)
    {
    Text text = Instantiate(textToast, new Vector3(transform.position.x, transform.position.y, 0) , Quaternion.identity);
    text.text = pointsValue.ToString();
    }
}
