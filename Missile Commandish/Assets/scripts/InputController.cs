using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class InputController *
 * Handles player input.
*******************************************************************************/
public class InputController : MonoBehaviour
  {
  /*****************************************************************************
   * Unity Methods *
  *****************************************************************************/
  /*****************************************************************************
   * Update *
  *****************************************************************************/
  private void Update()
    {
    if(Input.GetKeyDown(KeyCode.M))
      GameManager.instance.voiceSoundManager.playMissilesDepleated();
    if(Input.GetKeyDown(KeyCode.D))
      GameManager.instance.voiceSoundManager.playDanger();
    if(Input.GetKeyDown(KeyCode.R))
      GameManager.restoreBuilding();

    if(!GameManager.instance.gamePaused)
      {
      /** Left Mouse click: Launch a Rocket. */
      if(Input.GetMouseButtonDown(0))
        {
        launchRocket();
        }
      }
    }

  /*****************************************************************************
   * Methods 
  *****************************************************************************/
  /*****************************************************************************
   * launchRocket *
   * Launch a rocket targeting the location of the mouse from the closest
   * Launcher to the mouse.
  *****************************************************************************/
  protected void launchRocket()
    {
    if(GameManager.instance.playerRocketCounter > 0)
      {
      /** This target is only used for determining if in the play area. The actual
       *  target is set in Player Targeter. */
      Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      /** Check to see if the click was in the play area. */
      bool inPlayArea = target.x <= GameManager.playAreaRightX && target.x >= GameManager.playAreaLeftX &&
                        target.y <= GameManager.playAreaTopY && target.y >= GameManager.playAreaBottomY;

      /** Find the active Launcher closest to the target point to fire from. */
      if(inPlayArea)
        {
        SpawnPoint launcherToUse = null;
        float shortestDist = 1000f;
        GameObject[] spawnPoints = GameManager.instance.launchers;

        foreach(GameObject l in spawnPoints)
          {
          float d = Vector3.Distance(target, l.transform.position);
          if(shortestDist > d && l.activeInHierarchy)
            {
            launcherToUse = l.GetComponent<SpawnPoint>();
            shortestDist = d;
            }
          }

        /** Launch a rocket. */
        if(launcherToUse != null)
          {
          launcherToUse.spawn();
          }
        }      
      }
    }
  }
