using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class ShowByCondition : MonoBehaviour {
	GameObject content;
	public bool executeInEditMode = false;

	public GameObject Content {
		get {
			if (content == null) {
				content = transform.Children()[0].gameObject;
			}
			return content;
		}
	}

	public BoolValueProvider condition;

	public void Awake() {
		if (condition == null) {
			condition = GetComponent<BoolValueProvider>();
		}
	}

	public void Update() {
		if (executeInEditMode || !Extensions.InEditMode()) {
			Content.SetActive(condition.Value);
		}
	}
}
