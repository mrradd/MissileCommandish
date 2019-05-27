using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
  /** In Game UI Canvas. */
  public Canvas inGameUICanvas;

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

  /** Main Camera Shaker. */
  public GameObject mainCameraShaker;

  /** Main game. */
  public GameObject mainGameObject;

  /** Pause Camera. */
  public Camera pauseCamera;

  /** Pause Menu. */
  public GameObject pauseMenuObject;

  /** UI manager for the pause menu. */
  public PauseMenuUIManager pauseMenuUIManager;

  /** Points toast. */
  public Text pointsToast;

  [Header("Speed Modifier")]
  public int speedMod;
  public int speedModStep;
  public int speedModMax;

  [Header("Spawnpoints")]
  /** Spawn points for Bombers. */
  public SpawnPoint[] bomberSpawnPoints;

  /** Spawn points for Enemy Rockets. */
  public SpawnPoint[] enemyRocketSpawnPoints;

  /** Spawn points for MIRVs. */
  public SpawnPoint[] mirvSpawnPoints;

  /** Reticle spawner. */
  public SpawnPoint reticleSpawner;

  [Header("Buildings")]
  /** City list. */
  public GameObject[] cities;

  /** Spawn points for Player Rockets. */
  public GameObject[] launchers;

  /** Destroyed buildings. */
  public List<Building> destroyedBuildings = new List<Building>();

  [Header("Stats")]
  /** Active Enemy threats counter. */
  public int activeEnemyWeapons = 0;

  /** Qty of bombers per level. */
  public int bomberCount = 0;

  /** Max number of bombers. */
  public int bomberMax;

  /** Current wave/level. */
  public int currentWave = 1;

  /** Enemy weapon counter. */
  public int enemyWeaponCounter = 0;

  /** Max number of Enemy weapons. */
  public int maxEnemyWeaponCount = 20;

  /** Qty of mirvs per level. */
  public int mirvCount = 0;

  /** Max mirvs per level. */
  public int mirvMax;

  /** Max number of Plyer weapons. */
  public int maxPlayerRocketCount = 30;

  /** How many Rockets the Player has. */
  public int playerRocketCounter = 0;

  /** Current Player Score. */
  public int playerScore = 0;

  /** Threshold to meet to regen a building. */
  public int reviveBuildingScoreThreshold = 5000;

  /** Counter tracking points until building is revived. */
  public int reviveBuildingScore = 0;

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
  public bool gameLost;
  public bool playedMissilesDepleated;
  public bool playedDanger;
  public bool cityRestored;

  [Header("Audio")]
  public VoiceSoundManager voiceSoundManager;
  public AudioSource audioSource;

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

  /** Returns the Main Camera Shaker. */
  public static CameraShaker getMainCameraShaker()
    {
    return instance.mainCameraShaker.GetComponent<CameraShaker>();
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
    /** HACK: For some reason I couldn't get the voice sounds prefab AudioSource
     * to be seen as active, so I am setting one here. */
    voiceSoundManager.audioSource = audioSource;

    instance.gamePaused = false;

    startNextLevel();
    }

  /*****************************************************************************
   * Update *
  *****************************************************************************/
  private void Update()
    {
    if(!instance.gamePaused)
      {
      /** Play no missile warning. */
      if(!instance.playedMissilesDepleated && instance.playerRocketCounter <= 0)
        {
        instance.playedMissilesDepleated = instance.voiceSoundManager.playMissilesDepleated();
        }

      /** Play danger warning. */
      if (!instance.playedDanger && activeCityCount <= 1)
        {
        instance.playedDanger = instance.voiceSoundManager.playDanger();
        }
      }

    if(instance.mainCamera.gameObject.activeSelf)
      {
      if(checkWin())
        {
        levelCleared();
        }

      if(checkLose())
        {
        gameOver();
        }
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
    return instance.mNoCitiesLeft;
    }

  /*****************************************************************************
   * checkWin *
   * Verifies if the Player won. (Only checks if main camera is active meaning
   * the game is being played.
  *****************************************************************************/
  public static bool checkWin()
    {
    return instance.mainCamera.gameObject.activeSelf &&
           instance.enemyWeaponCounter <= 0          &&
           activeCityCount > 0                       &&
           instance.activeEnemyWeapons <= 0;
    }

  /*****************************************************************************
   * destroyAllButOneCity *
   * Destroys all cities but 1 random one.
  *****************************************************************************/
  public void destroyAllButOneCity()
    {
    int toSpare = (int)Mathf.Round(Random.Range(0f, 3f));

    /** Destory cities. */
    for(int i = 0; i < instance.cities.Length; i++)
      {
      if(i == toSpare)
        continue;
      
      Debug.Log("City " + i + " destoryed.");
      instance.cities[i].GetComponent<Building>().initDestroyedVesion(false);
      }
    }

  /*****************************************************************************
   * gameOver *
   * Transitions to the Game Over screen.
  *****************************************************************************/
  public static void gameOver()
    {
    instance.gameLost = true;

    instance.mTransitionTimer += Time.deltaTime;

    if(instance.mTransitionTimer >= instance.levelClearedTimeLimit && instance.activeEnemyWeapons <= 0)
      {
      Debug.Log("gameOver");
      saveFinalStats();
      SceneManager.LoadScene("GameOverScene");
      }
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

      restoreLaunchers();

      toggleCamera(3);
      }
    }

  /*****************************************************************************
   * handleMainMenu *
   * Transitions to the Main Menu.
  *****************************************************************************/
  public static void mainMenu()
    {
    Debug.Log("mainMenu");
    PlayerPrefs.SetInt("ContinuingGame", 0);
    SceneManager.LoadScene("MainMenuScene");
    }

  /*****************************************************************************
   * restartGame *
   * Restarts the game over.
  *****************************************************************************/
  public static void restartGame()
    {
    Debug.Log("restartGame");
    PlayerPrefs.SetInt("ContinuingGame", 0);
    SceneManager.LoadScene("MainGameScene");
    }

  /*****************************************************************************
   * restoreCity *
   * Tries to restore a destroyed City.
  *****************************************************************************/
  public static void restoreCity()
    {
    bool buildingRestored = false;

    /** Try to restore a city. */
    if(!buildingRestored)
      {
      for(int i = 0; i < instance.cities.Length; i++)
        {
        if(!instance.cities[i].gameObject.activeSelf)
          {
          Debug.Log("City restored.");
          instance.cities[i].gameObject.SetActive(true);
          Destroy(instance.cities[i].GetComponent<Building>().destroyedVersion);
          buildingRestored      = true;
          instance.cityRestored = true;
          break;
          }
        }
      }

    if(!buildingRestored)
      Debug.Log("Didn't restore city.");
    }

  /*****************************************************************************
   * restoreLaunchers *
   * Restors all destroyed Launchers.
  *****************************************************************************/
  public static void restoreLaunchers()
    {
    /** Try to restore a launcher. */
    for (int i = 0; i < instance.launchers.Length; i++)
      {
      if (!instance.launchers[i].gameObject.activeSelf)
        {
        Debug.Log("Launcher restored.");
        instance.launchers[i].gameObject.SetActive(true);
        Destroy(instance.launchers[i].GetComponent<Building>().destroyedVersion);
        }
      }
    }

  /*****************************************************************************
   * saveFinalStats *
   * Saves the final stats like score and reason for losing.
  *****************************************************************************/
  public static void saveFinalStats()
    {
    PlayerPrefs.SetInt("PlayerScore", instance.playerScore);
    PlayerPrefs.SetInt("LastWavePlayed", instance.currentWave);
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
    if(PlayerPrefs.GetInt("ContinuingGame") == 1)
      {
      PlayerPrefs.SetInt("ContinuingGame", 0);
      instance.playerScore = PlayerPrefs.GetInt("PlayerScore");
      instance.currentWave = PlayerPrefs.GetInt("LastWavePlayed");
      instance.inGameUIManager.updatePlayerScoreText();

      /** Only start out with one city. */
      instance.destroyAllButOneCity();
      }
    else
      {
      instance.currentWave++;
      }

    instance.mTransitionTimer        = 0f;
    instance.enemyWeaponCounter      = instance.maxEnemyWeaponCount;
    instance.playerRocketCounter     = instance.maxPlayerRocketCount;
    instance.playedDanger            = false;
    instance.playedMissilesDepleated = false;
    instance.cityRestored            = false;

    int limit = (instance.currentWave / 3) + 2;
    instance.mirvCount   = limit > instance.mirvMax ? instance.mirvMax : limit;
    instance.bomberCount = limit > instance.bomberMax ? instance.bomberMax : limit;

    instance.inGameUIManager.updatePlayerRocketCountText(instance.maxPlayerRocketCount);
    instance.inGameUIManager.updateCurrentWaveText();

    updateSpeedModifier();

    getMainCameraShaker().zeroOutShakeTimer();

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
    }

  /*****************************************************************************
   * updatePlayerScore *
   * Increments Player's score by passed in value.
   * @param  value  Value to adjust score by.
  *****************************************************************************/
  public static void updatePlayerScore(int value)
    {
    instance.playerScore = instance.playerScore + value;
    instance.reviveBuildingScore = instance.reviveBuildingScore + value;
    instance.inGameUIManager.updatePlayerScoreText();
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

  /*****************************************************************************
   * updateSpeedModifier *
   * Updates the speed modifier for weapons.
  *****************************************************************************/
  public static void updateSpeedModifier()
    {
    int mod = instance.currentWave * instance.speedModStep;
    instance.speedMod = mod > instance.speedModMax ? instance.speedMod : mod;
    Debug.Log("SpeedMod: " + instance.speedMod);
    }
  }
