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
  /** Audio source. */
  public AudioSource audioSource;

  /** List of launch sounds. */
  public AudioClip[] audioClips;

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
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * playSound *
   * Plays a random sound.
  *****************************************************************************/
  public void playSound()
    {
    int i = (int)Random.Range(0, audioClips.Length);
    audioSource.PlayOneShot(audioClips[i]); 
    }
  }
