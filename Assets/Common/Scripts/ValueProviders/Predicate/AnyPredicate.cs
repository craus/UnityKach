using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AnyPredicate<T> : AggregatePredicate<T>
{
    public override Func<T, bool> CalculateValue => x => Arguments.Any(a => a(x));
}
