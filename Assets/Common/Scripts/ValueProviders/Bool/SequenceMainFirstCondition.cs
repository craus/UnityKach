using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceMainFirstCondition : BoolValueProvider
{
    [SerializeField] public List<AggregateCondition> conditions;

    private bool mainDoneFirst;
    public override bool CalculateValue {
        get
        {
            if (!mainDoneFirst)
            {
                for (int i = 1; i < conditions.Count; i++)
                {
                    if (conditions[i].Value)
                        return false;
                }

                if (conditions[0].Value)
                {
                    mainDoneFirst = true;
                    return true;
                }

                return false;
            }

            return true;
        }
    }
}
