using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class Counter : IntValueProvider {
	[SerializeField] private int value;

	public override int CalculateValue {
        get { return value; }
    }

    public int CounterValue {
        get { return value; }
        set {
            if (this.value == value) {
                return;
            }
            this.value = value;
            OnChange();
        }
    }

    public int minValue = int.MinValue;
    public int maxValue = int.MaxValue;

    public int startValue = 0;

    public UnityEvent onChange;
    public UnityEvent onMax;
    public UnityEvent onMin;
    public UnityEvent onIncrement;
    public UnityEvent onZero;
    public UnityEvent onDecrement;


	public void SetValue(int value) {
		this.value = value;
	}

    public void ResetValue() {
        value = startValue;
        onChange.Invoke();
    }

    private void OnChange() {
        onChange.Invoke();
        if (value == 0) {
            onZero.Invoke();
        }
        if (value == minValue) {
            onMin.Invoke();
        }
        if (value == maxValue) {
            onMax.Invoke();
        }
    }

    public void Decrement() {

        if (value == minValue) {
            return;
        }
        value -= 1;

        onDecrement.Invoke();
        OnChange();
    }

    public void Increment() {
        if (value == maxValue) {
            return;
        }
        value += 1;
 
        onIncrement.Invoke();
        OnChange();
    }

    public void Drop() {
        value = 0;
        OnChange();
    }

    public void Add(int delta) {
        value += delta;
        value = Mathf.Clamp(value, 0, maxValue);
        OnChange();
    }

    public void Add(Counter c) {
        Debug.LogFormat("Add");
        Add(c.value);
    }
}
