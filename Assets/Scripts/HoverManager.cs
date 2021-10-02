using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HoverManager : Singletone<HoverManager>
{
    public CreatureView hoveredCreatureView = null;

    public void UpdateHoveredCreatureView() {
        var worldCursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hoveredCreatureView = GameManager.instance.worldView.creatureViews.FirstOrDefault(c => c.ContainsPoint(worldCursor));
    }

    public void Update() {
        UpdateHoveredCreatureView();
    }
}