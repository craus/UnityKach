using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreatureView : View<Creature>
{
    public Transform mainTransform;

    public Image pieEat;
    public Image pieDie;

    public TMPro.TextMeshProUGUI levelText;

    public float BaseRadius => 1;

    public float Radius => BaseRadius * (1 + 0.25f * model.level);

    public bool ContainsPoint(Vector2 point) => Vector2.Distance(transform.position, point) < Radius;

    public void Update() {
        mainTransform.localScale = 2 * Radius * Vector3.one;

        pieEat.fillAmount = model.eat.spentPart;
        pieDie.fillAmount = model.die.spentPart;

        levelText.text = model.level.ToString();
    }

    public void MouseDown() {
        model.level++;
    }
}