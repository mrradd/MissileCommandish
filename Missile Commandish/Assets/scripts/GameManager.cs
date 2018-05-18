using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * GameManager *
 * Manages global game properties and behavior.
*******************************************************************************/
public class GameManager : MonoBehaviour
  {
  /** Instance of the GameManager. */
  public static GameManager instance;

  [Header("Spawnpoints")]
  public GameObject[] enemyRocketSpawnPoints;

  [Header("Player Stats")]
  /** How many Rockets the Player has. */
  public int playerRocketCount;

  /** Current Player Score. */
  public int playerScore;

  [Header("Prefabs")]
  /** Player's Standard Rocket. */
  public GameObject playerRocket;

  /** Enemy's Standard Rocket. */
  public GameObject enemyRocket;

  [Header("Misc")]
  /** Play area boundaries. */
  public GameObject leftBoundary;
  public GameObject rightBoundary;

  /*****************************************************************************
   * Unity Methods
  *****************************************************************************/
  /*****************************************************************************
   * Awake *
  *****************************************************************************/
  private void Awake()
    {
    if(!instance)
      instance = this;
    else
      {
      Debug.LogError("There can only be one instance of GameManager.");
      Destroy(gameObject);
      }
    }

  /*****************************************************************************
   * Update *
  *****************************************************************************/
  private void Update()
    {
    if(Input.GetKeyDown(KeyCode.Space))
      {
      int i = Random.Range(0, enemyRocketSpawnPoints.Length - 1);
      Debug.Log(i);
      GameObject g = Instantiate(enemyRocket, enemyRocketSpawnPoints[i].transform.position, Quaternion.identity);
      }
    }
  }
