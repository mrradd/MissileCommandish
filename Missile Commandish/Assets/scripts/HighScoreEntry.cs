using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * class HighScoreEntry *
 * Represents a high score entry.
*******************************************************************************/
public class HighScoreEntry
  {
  public string name;
  public int score;
  public int place;

  public HighScoreEntry() { name = "no name"; score = 0; place = 0; }
  public HighScoreEntry(string name, int score, int place)
    {
    this.name = name;
    this.score = score;
    this.place = place;
    }
  }
