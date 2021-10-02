using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class AggregateCondition : BoolValueProvider {
    public List<BoolValueProvider> arguments;
    public IEnumerableBoolProvider argumentsProvider;

    public IEnumerable<bool> Arguments => argumentsProvider != null ? argumentsProvider.Value : arguments.Select(a => a.Value);


    public override bool Cacheable => true;

    public virtual void Awake() {
        if (argumentsProvider != null) {
            argumentsProvider.onCacheInvalidated.AddListener(InvalidateCache);
        }
        arguments.ForEach(a => a.onCacheInvalidated.AddListener(InvalidateCache));
    }
}
