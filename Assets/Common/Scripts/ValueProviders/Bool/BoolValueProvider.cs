using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class BoolValueProvider : ValueProvider<bool>
{
    [SerializeField] private bool negated;
    
    [ContextMenu("Preview value")]
    public void PreviewValue()
    {
        PreviewValueCommand();
    }

    public override bool CalculateValue => BoolValue ^ negated;

    protected virtual bool BoolValue => false;
}
