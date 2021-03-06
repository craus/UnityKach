using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Common
{
    public class KeyUp : BoolValueProvider
    {
        [SerializeField] private KeyCode key;

        public override bool CalculateValue => Input.GetKeyUp(key);
    }
}
