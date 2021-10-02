using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Common
{
    public class IntCondition : BoolValueProvider
    {
        [SerializeField] private IntValueProvider source;
        public int threshold;

        public bool less;
        public bool equal;
        public bool greater;

        public override bool CalculateValue => 
            source.Value < threshold && less || 
            source.Value == threshold && equal || 
            source.Value > threshold && greater; 
    }
}
