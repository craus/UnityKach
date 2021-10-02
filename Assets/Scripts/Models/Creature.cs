using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Creature : Model
{
    /// <summary>
    /// Use view.transform.position whenever possible
    /// </summary>
    public Vector2 position;

    public float timerDuration;
    public float timerStart;

    public int level;
}