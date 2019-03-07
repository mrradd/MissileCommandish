using System.Collections;
using UnityEngine;
using UnityEngine.Monetization;

//http://unityads.unity3d.com/help/unity/integration-guide-unity

public class UnityAdsPlacement : MonoBehaviour
  {

  public string placementId = "video";

  public delegate void TestDelegate();

  TestDelegate method;

  public void ShowAd(TestDelegate test)
    {
    method = test;
    StartCoroutine(ShowAdWhenReady());
    }

  private IEnumerator ShowAdWhenReady()
    {
    while (!Monetization.IsReady(placementId))
      {
      yield return new WaitForSeconds(0.25f);
      }

    ShowAdPlacementContent ad = null;
    ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

    if (ad != null)
      {
      ad.Show(AdFinished);
      }
    }

  void AdFinished(ShowResult result)
    {
    if (result == ShowResult.Finished)
      {
      method();
      }
    }
}