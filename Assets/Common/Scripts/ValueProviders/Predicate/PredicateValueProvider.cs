using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class PredicateValueProvider<T> : ValueProvider<Func<T,bool>>
{
    [SerializeField] [ReadOnly] private bool boolPreview;
    [SerializeField] private T objectForPreview;
    
    [ContextMenu("Preview value")]
    public void PreviewValue()
    {
        PreviewValueCommand();
    }
    
    protected override void PreviewValueInternal() 
    {
        base.PreviewValueInternal();
        boolPreview = Value(objectForPreview);
        Debug.LogFormat($"Bool Value: {boolPreview}");
    }
}
