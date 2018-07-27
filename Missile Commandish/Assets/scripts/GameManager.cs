using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO CH  MAKE COUNTERS FOR ACTIVE CITIES, LAUNCHERS, AND ENEMY WEAPONS. OBJECTS SHOULD TAKE CARE OF UPDATING THE COUNTERS ON
//INSTANTIATION AND DESTRUCTION.
/*******************************************************************************
 * class GameManager *
 * Manages global game properties and behavior.
 * 
 * NOTE: An Enemy Weapon is considered active until its explosion animation is
 * finished. Bombers also deactivate when they reach their destination.
*******************************************************************************/
public class GameManager : MonoBehaviour
  {
  public static string PLAYER_EXPLOSION_TAG = "PlayerExplosion";
  public static string PLAYER_ROCKET_TAG    = "PlayerRocket";

  /** Instance of the GameManager. */
  public static GameManager instance;

  /** Active Enemy threats counter. */
  public int activeEnemyWeapons = 0;

  /** Current wave/level. */
  public int currentWave = 1;

  /** UI manager for the game. */
  public InGameUIManager inGameUIManager;

  /** Level Cleared Camera. */
  public Camera levelClearedCamera;

  /** UI manager for the level cleared menu. */
  public LevelClearedUIManager levelClearedUIManager;

  /** Main Camera. */
  public Camera mainCamera;

  /** Pause Camera. */
  public Camera pauseCamera;

  /** UI manager for the pause menu. */
  public PauseMenuUIManager pauseMenuUIManager;

  [Header("Enemy Spawnpoints")]
  /** Spawn points for Enemy Rockets. */
  public SpawnPoint[] enemyRocketSpawnPoints;

  [Header("Buildings")]
  /** City list. */
  public GameObject[] cities;

  /** Spawn points for Player Rockets. */
  public SpawnPoint[] launchers;

  [Header("Stats")]
  /** Enemy weapon counter. */
  public int enemyWeaponCounter = 0;

  /** Max number of Enemy weapons. */
  public int maxEnemyWeaponCount = 20;

  /** Max number of Plyer weapons. */
  public int maxPlayerRocketCount = 30;

  /** How many Rockets the Player has. */
  public int playerRocketCounter = 0;

  /** Current Player Score. */
  public int playerScore = 0;

  [Header("Boundaries")]
  /** Enemy Rocket target boundaries. */
  public GameObject leftBoundary;
  public GameObject rightBoundary;

  /** Play area boundaries. */
  public GameObject playAreaLeft;
  public GameObject playAreaRight;
  public GameObject playAreaTop;
  public GameObject playAreaBottom;

  [Header("Timers")]
  public float levelClearedTimeLimit;

  [Header("Flags")]
  public bool gamePaused;

  protected float mLevelClearedTimer;

  /*****************************************************************************
   * Accessors
  *****************************************************************************/
  /** Enemy Rocket Spawn Points */
  public static SpawnPoint[] enemyRocketSpawners { get { return instance.enemyRocketSpawnPoints; } }

  /** Boundaries. */
  /** Right X boundary. */
  public static float boundaryRightX  { get { return instance.rightBoundary.transform.position.x; } }

  /** Left X boundary. */
  public static float boundaryLeftX   { get { return instance.leftBoundary.transform.position.x; } }

  /** Floor Y boundary. */
  public static float floorY          { get { return instance.rightBoundary.transform.position.y; } }

  /** Play area left X boundary. */
  public static float playAreaLeftX   { get { return instance.playAreaLeft.transform.position.x;  }}

  /** Play area right X boundary. */
  public static float playAreaRightX  { get { return instance.playAreaRight.transform.position.x; } }

  /** Play area top Y boundary. */
  public static float playAreaTopY    { get { return instance.playAreaTop.transform.position.y; } }

  /** Play area bottom Y boundary. */
  public static float playAreaBottomY { get { return instance.playAreaBottom.transform.position.y; } }

  /** Returns active City count. */
  public static int activeCityCount
    {
    get
      {
      int activeCount = 0;
      for(int i = 0; i < instance.cities.Length; i++)
        activeCount += instance.cities[i].gameObject.activeSelf ? 1 : 0;

      return activeCount;
      }
    }

  /** Returns active Launcher count. */
  public static int activeLauncherCount
    {
    get
      {
      int activeCount = 0;
      for(int i = 0; i < instance.launchers.Length; i++)
        activeCount += instance.launchers[i].gameObject.activeSelf ? 1 : 0;

      return activeCount;
      }
    }

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
   * Start *
  *****************************************************************************/
  private void Start()
    {
    instance.enemyWeaponCounter  = instance.maxEnemyWeaponCount;
    instance.playerRocketCounter = instance.maxPlayerRocketCount;

    toggleCamera(1);

    instance.inGameUIManager.updatePlayerScoreText(instance.playerScore);
    instance.inGameUIManager.updateThreatCount(instance.enemyWeaponCounter);
    instance.inGameUIManager.updatePlayerRocketCountText(instance.maxPlayerRocketCount);
    }

  /*****************************************************************************
   * Update *
  *****************************************************************************/
  private void Update()
    {
    if(checkWin())
      handleLevelCleared();

    if(checkLose())
      Debug.Log("Level lost.");
    }

  /*****************************************************************************
   * Methods
  *****************************************************************************/
  /*****************************************************************************
   * checkLose *
   * Verifies if the Player won.
  *****************************************************************************/
  public static bool checkLose()
    {
    return activeCityCount <= 0 || activeLauncherCount <= 0;
    }

  /*****************************************************************************
   * checkWin *
   * Verifies if the Player won.
  *****************************************************************************/
  public static bool checkWin()
    {
    return instance.enemyWeaponCounter <= 0 && activeCityCount > 0 &&
                   activeLauncherCount > 0 && instance.activeEnemyWeapons <= 0 &&
                   instance.mainCamera.gameObject.activeSelf;
    }

  /*****************************************************************************
   * levelCleared *
   * Handles when the level is cleared.
  *****************************************************************************/
  public static void handleLevelCleared()
    {
    instance.mLevelClearedTimer += Time.deltaTime;

    /** Delay level cleared, so the transition is not abrupt. */
    if(instance.mLevelClearedTimer >= instance.levelClearedTimeLimit && instance.mainCamera.gameObject.activeSelf)
      {
      toggleCamera(3);
      }
    }

  /*****************************************************************************
   * restartGame *
   * Restarts the game over.
  *****************************************************************************/
  public static void restartGame()
    {
    SceneManager.LoadScene("MainGameScene");
    //instance.mLevelClearedTimer  = 0f;
    //instance.enemyWeaponCounter  = 0;
    //instance.playerRocketCounter = 0;
    //instance.playerScore         = 0;

    //instance.inGameUIManager.updateThreatCount(instance.enemyWeaponCounter);
    //instance.inGameUIManager.updatePlayerRocketCountText(instance.playerRocketCounter);
    //instance.inGameUIManager.updatePlayerScoreText(instance.playerScore);

    //toggleCamera(1);
    }

  /*****************************************************************************
   * startNextLevel *
   * Sets up the next level.
  *****************************************************************************/
  public static void startNextLevel()
    {
    instance.mLevelClearedTimer  = 0f;
    instance.enemyWeaponCounter  = instance.maxEnemyWeaponCount;
    instance.playerRocketCounter = instance.maxPlayerRocketCount;
    instance.currentWave++;

    //TODO CH  CONSIDER ADJUSTING WEAPON COUNTS PER LEVEL.

    instance.inGameUIManager.updateThreatCount(instance.enemyWeaponCounter);
    instance.inGameUIManager.updatePlayerRocketCountText(instance.playerRocketCounter);

    toggleCamera(1);
    }

  /*****************************************************************************
   * toggleCamera *
   * Toggles cameras on/off.
   * @param  camera  1:Main, 2:Pause, 3:Level Cleared
  *****************************************************************************/
  public static void toggleCamera(int camera)
    {
    instance.gamePaused = true;
    instance.mainCamera.gameObject.SetActive(false);
    instance.pauseCamera.gameObject.SetActive(false);
    instance.levelClearedCamera.gameObject.SetActive(false);

    switch(camera)
      {
      case 1:
        instance.mainCamera.gameObject.SetActive(true);
        instance.gamePaused = false;
        break;
      case 2:
        instance.pauseCamera.gameObject.SetActive(true);
        break;
      case 3:
        instance.levelClearedCamera.gameObject.SetActive(true);
        break;
      }
    }

  /*****************************************************************************
   * updateActiveEnemyWeaponCount *
   * Increments active Enemy Weapon counter by passed in value.
   * @param  value  Value to adjust count by.
  *****************************************************************************/
  public static void updateActiveEnemyWeaponCount(int value)
    {
    instance.activeEnemyWeapons = instance.activeEnemyWeapons + value;
    }

  /*****************************************************************************
   * updateEnemyWeaponCount *
   * Increments weapon counter by passed in value.
   * @param  value  Value to adjust count by.
  *****************************************************************************/
  public static void updateEnemyWeaponCount(int value)
    {
    instance.enemyWeaponCounter = instance.enemyWeaponCounter + value;
    instance.inGameUIManager.updateThreatCount(instance.enemyWeaponCounter);
    }

  /*****************************************************************************
   * updatePlayerScore *
   * Increments Player's score by passed in value.
   * @param  value  Value to adjust score by.
  *****************************************************************************/
  public static void updatePlayerScore(int value)
    {
    instance.playerScore = instance.playerScore + value;
    instance.inGameUIManager.updatePlayerScoreText(instance.playerScore);
    }

  /*****************************************************************************
   * updatePlayerRocketCount *
   * Increments Player's Rocket count passed in value.
   * @param  value  Value to adjust score by.
  *****************************************************************************/
  public static void updatePlayerRocketCount(int value)
    {
    instance.playerRocketCounter = instance.playerRocketCounter + value;
    instance.inGameUIManager.updatePlayerRocketCountText(instance.playerRocketCounter);
    }
  }
