using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureViewHovered : BoolValueProvider
{
    public CreatureView target;

    protected override bool BoolValue => HoverManager.instance.hoveredCreatureView == target;
}