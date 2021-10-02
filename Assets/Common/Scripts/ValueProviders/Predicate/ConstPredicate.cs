using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ConstPredicate<T> : PredicateValueProvider<T> {
    public bool constant;
    public void SetValue(bool value)
    {
        constant = value;
    }
    public override Func<T, bool> CalculateValue => x => constant;
}
