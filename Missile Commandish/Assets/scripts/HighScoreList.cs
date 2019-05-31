using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class HighScoreList *
 * Represents a list of high scores. This is kind of a shitty way to handle the
 * list, but I don't really care.
*******************************************************************************/
public class HighScoreList
  {
  public List<HighScoreEntry> highScores = new List<HighScoreEntry>();

  public HighScoreList()
    {
    for(int i = 1; i <= 5; i++)
      {
      highScores.Add(new HighScoreEntry(PlayerPrefs.GetString("HS" + i + "-Name").Length > 0 ? PlayerPrefs.GetString("HS" + i + "-Name") : "BOB",
                                        PlayerPrefs.GetInt("HS" + i + "-Score") > 1 ? PlayerPrefs.GetInt("HS" + i + "-Score") : 25001 - i * 3032,
                                        PlayerPrefs.GetInt("HS" + i + "-Place") > 1 ? PlayerPrefs.GetInt("HS" + i + "-Place") : i));
      }
    }
  }
