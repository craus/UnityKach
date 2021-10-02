using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Common
{
    public class IfElse : Effect
    {
        [SerializeField] private UnityEvent ifAction;
        [SerializeField] private UnityEvent elseAction;
        [SerializeField] private BoolValueProvider condition;

        public override void Run()
        {
            if (condition.Value)
            {
                ifAction.Invoke();
            }
            else
            {
                elseAction.Invoke();
            }
        }
    }
}