using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Common
{
    public class KeyDown : BoolValueProvider
    {
        [SerializeField] private KeyCode key;

        public override bool CalculateValue => Input.GetKeyDown(key);
    }
}
