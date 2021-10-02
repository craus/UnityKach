using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Common
{
    public class ConstantFloatProvider : FloatValueProvider
    {
        public float value;
        public override float CalculateValue => value;

        public void SetValue(float val) => value = val;
    }
}