using System;
using UnityEngine;

public class CheckInt : BoolValueProvider
{
    public IntValueProvider intProvider;
    public int Int => intProvider.Value;
    public int threshold;
    public bool greater;
    public bool less;
    public bool equal;

    public override bool CalculateValue =>
        greater && Int > threshold ||
        less && Int < threshold ||
        equal && Int == threshold;
}

