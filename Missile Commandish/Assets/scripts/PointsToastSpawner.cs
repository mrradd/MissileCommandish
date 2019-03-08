using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsToastSpawner : MonoBehaviour
  {
  public GameObject txtObj;

    // Update is called once per frame
    void Update()
      {
      if (Input.GetMouseButtonDown(1))
        {
        GameObject newGO = Instantiate(txtObj, gameObject.transform.position, Quaternion.identity);
        newGO.transform.SetParent(this.transform);
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newGO.transform.position = new Vector3(target.x, target.y, 0);
        newGO.transform.localScale = new Vector3(1,1,1);
        }
      }
  }
