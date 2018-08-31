using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class VoiceSoundManager *
 * Handles playing voice sounds.
*******************************************************************************/

public class VoiceSoundManager : MonoBehaviour
  {
  /** Audio source. */
  public AudioSource audioSource;

  /** Sound clips */
  public AudioClip danger;
  public AudioClip gameOver;
  public AudioClip missilesDepleated;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  /*****************************************************************************
   * Start
  *****************************************************************************/
  private void Start()
    {
    if(!audioSource)
      audioSource = new AudioSource();
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * playAmmoDepleated *
   * Plays danger warning.
   * 
   * @returns if sound was played or not.
  *****************************************************************************/
  public bool playDanger()
    {
    if(!audioSource.isPlaying)
      {
      audioSource.PlayOneShot(danger);
      return true;
      }

    return false;
    }

  /*****************************************************************************
   * playDanger *
   * Plays danger warning.
   * 
   * @returns if sound was played or not.
  *****************************************************************************/
  public bool playGameOver()
    {
    if(!audioSource.isPlaying)
      {
      audioSource.PlayOneShot(gameOver);
      return true;
      }

    return false;
    }

  /*****************************************************************************
   * playMissilesDepleated *
   * Plays ammo depleated warning.
   * 
   * @returns if sound was played or not.
  *****************************************************************************/
  public bool playMissilesDepleated()
    {
    if(!audioSource.isPlaying)
      {
      audioSource.PlayOneShot(missilesDepleated);
      return true;
      }

    return false;
    }
  }
