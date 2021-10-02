using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ListStringValueProvider : IEnumerableProvider<string>
{
    [ContextMenu("Preview value")]
    public void PreviewValue()
    {
        PreviewValueCommand();
    }
}
