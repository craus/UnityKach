using UnityEngine;

public class TriggerPressedValueProvider : BoolValueProvider
{
    [SerializeField] private string axis;
    [SerializeField, Range(0, 1)] private float pressedValue;
    public override bool CalculateValue => Input.GetAxis(axis) > pressedValue;
}
