using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controls : Singletone<Controls>
{
    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (HoverManager.instance.hoveredCreatureView != null) {
                HoverManager.instance.hoveredCreatureView.MouseDown();
            }
        }
    }
}