using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Common
{
    public class IntFormatter : StringValueProvider
    {
        [SerializeField] private IntValueProvider intProvider;
        [SerializeField] private string format = "{0}";

        public override string CalculateValue => intProvider.Value.ToString(format);
    }
}

