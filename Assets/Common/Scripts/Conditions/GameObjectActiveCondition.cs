using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Common
{
    public class GameObjectActiveCondition : BoolValueProvider
    {
        public override bool CalculateValue => gameObject.activeInHierarchy;
    }
}
