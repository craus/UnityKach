using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Creature : Model
{
    public const float BASE_EAT_TIME = 1;
    public const float BASE_DIE_TIME = 10;
    public const int SACRIFITION_DELTA = 3;

    /// <summary>
    /// Use view.transform.position whenever possible
    /// </summary>
    public Vector2 position;
    public int level;

    public Timer eat = new Timer(BASE_EAT_TIME, 0);
    public Timer die = new Timer(BASE_DIE_TIME, 0 + BASE_EAT_TIME);

    public Creature(float creationTime) {
        eat = new Timer(BASE_EAT_TIME, creationTime);
        die = new Timer(BASE_DIE_TIME, creationTime + BASE_EAT_TIME);
    }
}