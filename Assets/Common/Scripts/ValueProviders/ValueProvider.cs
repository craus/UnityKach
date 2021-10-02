using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;

public abstract class ValueProvider<T> : AbstractValueProvider {
    public T Value {
        get {
            if (!cacheValid) {
                RecalculateCache();
            }
            return cache;
        }
    }

    private void RecalculateCache() {
        var oldValue = cache;

        cache = CalculateValue;

        if (object.Equals(cache, oldValue)) {
            onValueChanged.Invoke();
        }

        if (Cacheable) {
            cacheValid = true;
        }
    }

    public abstract T CalculateValue { get; }

    public override object ObjectValue => Value;

    public event Action onChange;

    public void Changed() {
        if (onChange != null) {
            onChange.Invoke();
        }
    }

    public virtual bool Cacheable => false;
    
    [SerializeField] [ReadOnly] private bool cacheValid = false;
    [SerializeField] [ReadOnly] private T cache;

    [SerializeField] private bool autoUpdateCache = false;
    public UnityEvent onCacheInvalidated;
    public UnityEvent onValueChanged;

    public void InvalidateCache() {
        onCacheInvalidated.Invoke();
        cacheValid = false;
        if (autoUpdateCache) {
            _ = Value;
        }
    }

    #region value preview

    [SerializeField] [ReadOnly] private T valuePreview;
    [SerializeField] private bool enableValuePreview = false;

    protected virtual void PreviewValueCommand() {
        PreviewValueInternal();
        Debug.LogFormat($"Value: {Value}");
    }

    protected virtual void PreviewValueInternal() {
        valuePreview = Value;
    }

    protected virtual void Update()
    {
#if UNITY_EDITOR
        if (enableValuePreview)
        {
            PreviewValueInternal();
        }
#endif
	}

    #endregion
}
