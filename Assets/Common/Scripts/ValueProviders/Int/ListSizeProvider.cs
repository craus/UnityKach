using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ListSizeProvider : IntValueProvider {
    [SerializeField] private AbstractValueProvider source;
    public override int CalculateValue {
        get
        {
            var obj = source.ObjectValue;
            if (obj is IEnumerable list)
            {
                return list.Count();
            }
            throw new Exception("Wrong provider type!");
        }
    }
}
