using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class AggregatePredicate<T> : PredicateValueProvider<T> {
    public List<PredicateValueProvider<T>> arguments;

    public IEnumerable<Func<T, bool>> Arguments => arguments.Select(a => a.Value);

    public override bool Cacheable => true;

    public virtual void Awake() {
        arguments.ForEach(a => a.onCacheInvalidated.AddListener(InvalidateCache));
    }
}
