using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class RandomSoundManager *
 * Randomly plays a single sound from a list of sounds.
*******************************************************************************/
[RequireComponent(typeof(AudioSource))]
public class RandomSoundManager : MonoBehaviour
  {
  /** Audio source. */          public AudioSource audioSource;
  /** Played sound flag. */     public bool        playedSound = false;
  /** List of launch sounds. */ public AudioClip[] rocketLaunchSounds;

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

    playSound();
    }

  /*****************************************************************************
   * Update
  *****************************************************************************/
  private void Update()
    {
    
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * playSound *
   * Plays a random sound.
  *****************************************************************************/
  public void playSound()
    {
    if(!playedSound)
      {
      playedSound = true;
      int i = (int)Random.Range(0, rocketLaunchSounds.Length);
      audioSource.PlayOneShot(rocketLaunchSounds[i]);
      }
    }

  /*****************************************************************************
   * reset *
   * Resets the flag to allow for playing on update.
  *****************************************************************************/
  public void reset()
    {
    playedSound = false;
    }
  }
