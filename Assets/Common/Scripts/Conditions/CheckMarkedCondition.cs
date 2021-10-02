using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

namespace Common
{
    public class CheckMarkedCondition : BoolValueProvider
    {
        [SerializeField] private Mark mark;
        [SerializeField] private bool on = true;

        private bool MarkedConditionValue => MarkedCondition != null && MarkedCondition.Value;

        private BoolValueProvider markedCondition;
        private BoolValueProvider MarkedCondition
        {
            get
            {
                if (markedCondition == null)
                {
                    markedCondition = mark.objects.Select(marker => marker.GetComponent<BoolValueProvider>()).FirstOrDefault(c => c != null);
                }
                return markedCondition;
            }
        }

        public override bool CalculateValue => on == MarkedConditionValue;
    }
}
