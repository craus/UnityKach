using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Timer
{
    public float duration;
    public float start;

    public float spent => Time.time - start;

    public float spentPart => spent / (duration + float.Epsilon);

    public float finish => start + duration;

    public Timer(float duration, float start) {
        this.duration = duration;
        this.start = start;
    }
}