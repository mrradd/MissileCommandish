using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*******************************************************************************
 * class GameManager *
 * Manages global game properties and behavior.
 * 
 * NOTE: An Enemy Weapon is considered active until its explosion animation is
 * finished. Bombers also deactivate when they reach their destination.
*******************************************************************************/
public class GameManager : MonoBehaviour
  {
  public static string ENEMY_EXPLOSION_TAG  = "EnemyExplosion";
  public static string PLAYER_EXPLOSION_TAG = "PlayerExplosion";
  public static string PLAYER_ROCKET_TAG    = "PlayerRocket";

  /** Instance of the GameManager. */
  public static GameManager instance;

  [Header("Misc")]
  /** UI manager for the game. */
  public InGameUIManager inGameUIManager;

  /** Level Cleared Camera. */
  public Camera levelClearedCamera;

  /** Level cleared. */
  public GameObject levelClearedObject;

  /** UI manager for the level cleared menu. */
  public LevelClearedUIManager levelClearedUIManager;

  /** Main Camera. */
  public Camera mainCamera;

  /** Main game. */
  public GameObject mainGameObject;

  /** Pause Camera. */
  public Camera pauseCamera;

  /** Pause Menu. */
  public GameObject pauseMenuObject;

  /** UI manager for the pause menu. */
  public PauseMenuUIManager pauseMenuUIManager;

  [Header("Enemy Spawnpoints")]
  /** Spawn points for Bombers. */
  public SpawnPoint[] bomberSpawnPoints;

  /** Spawn points for Enemy Rockets. */
  public SpawnPoint[] enemyRocketSpawnPoints;

  /** Spawn points for MIRVs. */
  public SpawnPoint[] mirvSpawnPoints;

  [Header("Buildings")]
  /** City list. */
  public GameObject[] cities;

  /** Spawn points for Player Rockets. */
  public GameObject[] launchers;

  [Header("Stats")]
  /** Active Enemy threats counter. */
  public int activeEnemyWeapons = 0;

  /** Current wave/level. */
  public int currentWave = 1;

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

  [Header("Bonus Values")]
  /** Bonus points for remaining cities. */
  public int cityBonus;

  /** Bonus points for remaining launchers. */
  public int launcherBonus;

  /** Bonus points for remaining player rockets. */
  public int rocketBonus;

  [Header("Boundaries")]
  /** Enemy Rocket target boundaries. */
  public GameObject leftBoundary;
  public GameObject rightBoundary;

  public GameObject mirvSplitThreshold;

  /** Play area boundaries. */
  public GameObject playAreaLeft;
  public GameObject playAreaRight;
  public GameObject playAreaTop;
  public GameObject playAreaBottom;

  [Header("Timers")]
  public float levelClearedTimeLimit;

  [Header("Flags")]
  public bool gamePaused;

  protected float mTransitionTimer;
  protected bool  mNoCitiesLeft;
  protected bool  mNoLaunchersLeft;

  /*****************************************************************************
   * Accessors
  *****************************************************************************/
  /** Enemy Rocket Spawn Points */
  public static SpawnPoint[] bomberSpawners { get { return instance.bomberSpawnPoints; } }

  /** Enemy Rocket Spawn Points */
  public static SpawnPoint[] enemyRocketSpawners { get { return instance.enemyRocketSpawnPoints; } }

  /** MIRV Spawn Points. */
  public static SpawnPoint[] mirvRocketSpawners { get { return instance.mirvSpawnPoints; } }

  /** Boundaries. */
  /** Right X boundary. */
  public static float boundaryRightX  { get { return instance.rightBoundary.transform.position.x; } }

  /** Left X boundary. */
  public static float boundaryLeftX   { get { return instance.leftBoundary.transform.position.x; } }

  /** Floor Y boundary. */
  public static float floorY          { get { return instance.rightBoundary.transform.position.y; } }

  /** MIRV Y boundary. */
  public static float mirvThresholdY  { get { return instance.mirvSplitThreshold.transform.position.y; } }

  /** Play area left X boundary. */
  public static float playAreaLeftX   { get { return instance.playAreaLeft.transform.position.x;  }}

  /** Play area right X boundary. */
  public static float playAreaRightX  { get { return instance.playAreaRight.transform.position.x; } }

  /** Play area top Y boundary. */
  public static float playAreaTopY    { get { return instance.playAreaTop.transform.position.y; } }

  /** Play area bottom Y boundary. */
  public static float playAreaBottomY { get { return instance.playAreaBottom.transform.position.y; } }

  /** Check active. */
  /** Returns a list of all active buildings. */
  public static List<GameObject> activeBuildings
    {
    get
      {
      List<GameObject> list = new List<GameObject>();

      for(int i = 0; ; i++)
        {
        /** Add cities to the list. */
        if(i < instance.cities.Length && instance.cities[i].activeSelf)
          {
          list.Add(instance.cities[i]);
          }
          
        /** Add launchers to the list. */
        if(i < instance.launchers.Length && instance.launchers[i].activeSelf)
          {
          list.Add(instance.launchers[i]);
          }

        if(i > instance.cities.Length && i > instance.launchers.Length)
          break;
        }

      return list;
      }
    }

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
    if(!instance.gamePaused)
      {
      if(checkWin())
        levelCleared();

      if(checkLose())
        gameOver();      
      }
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
    instance.mNoCitiesLeft = activeCityCount <= 0;
    instance.mNoLaunchersLeft = activeLauncherCount <= 0;
    return instance.mNoCitiesLeft || instance.mNoLaunchersLeft;
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
  public static void levelCleared()
    {
    instance.mTransitionTimer += Time.deltaTime;

    /** Delay level cleared, so the transition is not abrupt. */
    if(instance.mTransitionTimer >= instance.levelClearedTimeLimit && instance.mainCamera.gameObject.activeSelf)
      {
      Debug.Log("levelCleared");
      instance.levelClearedUIManager.updateText();
      toggleCamera(3);
      }
    }

  /*****************************************************************************
   * gameOver *
   * Transitions to the Game Over screen.
  *****************************************************************************/
  public static void gameOver()
    {
    instance.mTransitionTimer += Time.deltaTime;

    if(instance.mTransitionTimer >= instance.levelClearedTimeLimit)
      {
      Debug.Log("gameOver");
      saveFinalStats();
      SceneManager.LoadScene("GameOverScene");      
      }
    }

  /*****************************************************************************
   * handleMainMenu *
   * Transitions to the Main Menu.
  *****************************************************************************/
  public static void mainMenu()
    {
    Debug.Log("mainMenu");
    SceneManager.LoadScene("MainMenuScene");
    }

  /*****************************************************************************
   * restartGame *
   * Restarts the game over.
  *****************************************************************************/
  public static void restartGame()
    {
    Debug.Log("restartGame");
    SceneManager.LoadScene("MainGameScene");
    }

  /*****************************************************************************
   * saveFinalStats *
   * Saves the final stats like score and reason for losing.
  *****************************************************************************/
  public static void saveFinalStats()
    {
    PlayerPrefs.SetInt("PlayerScore", instance.playerScore);
    PlayerPrefs.SetInt("NoCitiesLeft", instance.mNoCitiesLeft ? 1 : 0);
    PlayerPrefs.SetInt("NoLaunchersLeft", instance.mNoLaunchersLeft ? 1 : 0);
    PlayerPrefs.Save();
    }

  /*****************************************************************************
   * startNextLevel *
   * Sets up the next level.
  *****************************************************************************/
  public static void startNextLevel()
    {
    instance.mTransitionTimer  = 0f;
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

    instance.pauseMenuObject.SetActive(false);
    instance.levelClearedObject.SetActive(false);
    instance.mainGameObject.SetActive(false);

    switch(camera)
      {
      case 1:
        Debug.Log("toggleCamera " + "1");
        instance.mainCamera.gameObject.SetActive(true);
        instance.mainGameObject.SetActive(true);
        instance.gamePaused = false;
        break;
      case 2:
        Debug.Log("toggleCamera " + "2");
        instance.pauseCamera.gameObject.SetActive(true);
        instance.pauseMenuObject.SetActive(true);
        break;
      case 3:
        Debug.Log("toggleCamera " + "3");
        instance.levelClearedCamera.gameObject.SetActive(true);
        instance.levelClearedObject.SetActive(true);
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
