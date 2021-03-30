using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerScore
{
    public string name;
    public int score;
}
[Serializable]
public class ScoreList
{
    public PlayerScore[] players;
}