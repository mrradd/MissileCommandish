using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
  {
  float frameCount = 0;
  float dt = 0;
  int fps = 0;
  float updateRate = 4;  // 4 updates per sec.

  public Text fpsText;

  void Update()
    {
    frameCount++;
    dt += Time.deltaTime;
    if(dt > 1.0f / updateRate)
      {
      fps = (int)Math.Floor(frameCount / dt);
      fpsText.text = "FPS " + fps;
      frameCount = 0f;
      dt -= 1.0f / updateRate;
      }
    }
  }
