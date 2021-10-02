using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class IntValueProvider : ValueProvider<int> {
    [ContextMenu("Preview value")]
    public void PreviewValue()
    {
        PreviewValueCommand();
    }
}
