using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class StringAnyPredicate : AnyPredicate<string>
{
    [ContextMenu("Add script")]
    public void AddScript() {
        gameObject.AddComponent<AnyPredicate<string>>();
    }
}
