using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class StringValueProvider : ValueProvider<string>
{
    [ContextMenu("Preview value")]
    public void PreviewValue() {
        PreviewValueCommand();
    }
}
