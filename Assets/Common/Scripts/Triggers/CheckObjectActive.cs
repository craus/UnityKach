using UnityEngine;

namespace Common
{
    public class CheckObjectActive : BoolValueProvider
    {
        public GameObject target;
        public bool active;
        public override bool CalculateValue => target != null ? target.activeSelf == active : false;
    }
}
