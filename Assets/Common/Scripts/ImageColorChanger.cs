using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ImageColorChanger : MonoBehaviour {
	public ColorProvider provider;

	[SerializeField] private new Image image;

	[SerializeField] private bool checkOnUpdate = true;

	public void Awake() {
		image = GetComponent<Image>();
		provider = GetComponent<ColorProvider>();
	}

	public void Start() {
		provider.onChange += ProviderOnChange;
		ProviderOnChange();
	}

	void ProviderOnChange() {
		UpdateColor();
	}

	[ContextMenu("Update color")]
	public void UpdateColor() {
		if (image != null) {
			image.color = provider.Value;
		}
	}

	private void Update() {
		if (checkOnUpdate) {
			UpdateColor();
		}
	}
}
