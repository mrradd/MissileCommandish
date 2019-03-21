using UnityEngine;
using UnityEngine.Monetization;

//http://unityads.unity3d.com/help/unity/integration-guide-unity

public class UnityAdsScript : MonoBehaviour
  {

  public string androidGameId = "3081374";
  public string iosGameID = "3081375";
  public bool   testMode = false;

  void Start()
    {
    /** Assuming we are only on android or ios. */
    string id = Application.platform == RuntimePlatform.Android ? androidGameId : iosGameID;
    Monetization.Initialize(id, testMode);
    }
  }