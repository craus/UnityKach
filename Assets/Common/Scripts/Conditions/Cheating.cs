using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Common
{
    public class Cheating : BoolValueProvider
    {
        protected override bool BoolValue => Cheats.on;
    }
}
