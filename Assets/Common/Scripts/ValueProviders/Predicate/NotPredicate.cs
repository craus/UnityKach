using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class NotPredicate<T> : PredicateValueProvider<T>
{
    public PredicateValueProvider<T> argument;

    public override Func<T, bool> CalculateValue => x => !argument.Value(x);
}
