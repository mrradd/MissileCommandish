using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
  {
  void Awake()
    {
    if(Application.platform == RuntimePlatform.WebGLPlayer)
      {
      SceneManager.LoadScene("MainMenuSceneWebGL");
      }
    else
      {
      SceneManager.LoadScene("MainMenuScene");
      }
    }
  }
