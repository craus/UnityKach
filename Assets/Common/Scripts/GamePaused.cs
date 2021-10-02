using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using RSG;

public class GamePaused : BoolValueProvider
{
    protected override bool BoolValue => TimeManager.instance.Paused;
}
