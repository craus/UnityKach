using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsPanel : Singletone<StatisticsPanel>
{
    public Text unitsCountText;
    public Text starvedCountText;
    public Text levelupsCountText;
    public Text timeText;
    public Text currentMissionText;

    public void Update() {
        unitsCountText.text = $"{GameManager.instance.worldView.creatureViews.Count}";
        //starvedCountText.text = $"{GameManager.instance.worldView.creatureViews.Count}";
        //levelupsCountText.text = $"{GameManager.instance.worldView.creatureViews.Count}";
        //timeText.text = $"{GameManager.instance.worldView.creatureViews.Count}";
        //currentMissionText.text = $"{GameManager.instance.worldView.creatureViews.Count}";
    }
}