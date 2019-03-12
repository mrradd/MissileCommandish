using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class TextToast *
 * A Text object that is appended to the Main Game Canvas. It spawns at its
 * target, floatin up slowly, displaying its given value, and destroys itself
 * after a given time.
*******************************************************************************/
public class TextToast : MonoBehaviour
  {
  public float   destroyTimer = 2;
  public int     speed        = 7;
  protected bool mIsSet       = false;

  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * Start *
  *****************************************************************************/
  void Start()
    {
    Destroy(gameObject, destroyTimer);
    
    transform.SetParent(GameManager.instance.inGameUICanvas.transform);
    }

  /*****************************************************************************
   * Update *
  *****************************************************************************/
  void Update()
    {
    if(!mIsSet)
      {
      transform.localScale = new Vector3(.75f, .75f, .75f);
      mIsSet = true;
      }

    moveUp();
    }

  /*****************************************************************************
   * Methods *
  *****************************************************************************/
  /*****************************************************************************
   * moveUp *
  *****************************************************************************/
  protected void moveUp()
    {
    transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speed, 0);
    }

  }
