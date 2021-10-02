using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldView : View<World>
{
    public CreatureView creatureViewSample;

    public List<CreatureView> creatureViews = new List<CreatureView>();

    public void Update() {
        if (!creatureViews.Any(c => c.model.level == 0)) {
            var newCreature = Instantiate(creatureViewSample);
            creatureViews.Add(newCreature);
            newCreature.transform.SetParent(transform);
            newCreature.Born();
        }
    }
}