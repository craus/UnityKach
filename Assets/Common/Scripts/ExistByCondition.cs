using System;
using UnityEngine;

public class ExistByCondition : MonoBehaviour
{
    public BoolValueProvider condition;

    public void Awake() {
        if (!condition.Value) {
            Destroy(gameObject);
        }
    }
}

