using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AllPredicate<T> : AggregatePredicate<T> {
    public override Func<T, bool> CalculateValue => x => Arguments.All(a => a(x));
}
