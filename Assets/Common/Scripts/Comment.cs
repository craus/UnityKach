using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR

/// <summary>
/// To make comments about interesting things in scenes/prefabs
/// </summary>
public class Comment : MonoBehaviour {

    [TextArea(2, 10)]
	[SerializeField] private string comment;

}

#endif