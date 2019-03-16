using UnityEngine;
using UnityEngine.Monetization;

//http://unityads.unity3d.com/help/unity/integration-guide-unity

public class UnityAdsScript : MonoBehaviour
  {

  public string gameId   = "3081374"; //Android
  public bool   testMode = false;

  void Start()
    {
    Monetization.Initialize(gameId, testMode);
    }
  }