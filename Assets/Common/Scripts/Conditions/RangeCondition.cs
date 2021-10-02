using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Common
{
    public class RangeCondition : BoolValueProvider
    {
        [SerializeField] private FloatValueProvider source;
        public float min;
        public float max;

        public bool inside = true;

        public override bool CalculateValue => (min < source.Value && source.Value < max) == inside;
    }
}
