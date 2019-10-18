using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerInformation 
{
    public int slotNumber;
    public string playerName;
    public int bossesDefeated;
    public int playedTime;
    public int microphonePressedTimes;
    public int microphonePressedTimesSuccesses;

    public int timesPlayedModernPaints;
    public int timesLossedModernPaints;

    public int timesPlayedOrchesta;
    public int timesLossedOrchesta;

    public int timesPlayedLoveGame;
    public int timesLossedLoveGame;

    public int timesPlayedWorms;
    public int timesLossedWorms;

    public int timesPlayeRPG;
    public int timesLossedRPG;

    public List<string> _pronouncedWordsLove = new List<string>();
    public List<string> _pronouncedWordsPaint = new List<string>();
    public List<string> _pronouncedWordsOrchesta = new List<string>();
    public List<string> _pronouncedWordsWorms = new List<string>();
    public List<string> _pronouncedWordsBoss = new List<string>();

}
