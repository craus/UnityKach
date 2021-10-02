using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureView : View<Creature>
{
    public Transform mainTransform;

    public void Update() {
        mainTransform.localScale = Vector3.one * (1 + 0.25f * model.level);
    }
}