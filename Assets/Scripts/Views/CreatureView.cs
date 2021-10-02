using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreatureView : View<Creature>
{
    public const float MIN_DISTANCE_TO_OTHERS = 1000;

    public Transform mainTransform;

    public Image pieEat;
    public Image pieDie;

    public TMPro.TextMeshProUGUI levelText;

    public float BaseRadius => 1;

    public float Radius => BaseRadius * (1 + 0.25f * model.level);

    public bool ContainsPoint(Vector2 point) => Vector2.Distance(transform.position, point) < Radius;

    public ParticleSystem explosionSample;

    public void Update() {
        mainTransform.localScale = 2 * Radius * Vector3.one;

        pieEat.fillAmount = model.eat.spentPart;
        pieDie.fillAmount = model.die.spentPart;

        levelText.text = model.level.ToString();

        if (model.die.spentPart > 1) {
            Die();
        }
    }

    public void MouseDown() {
        if (model.eat.spentPart < 1) {
            return;
        }
        if (model.level >= Creature.SACRIFITION_DELTA) {
            var sacrifition = Sacrifition;
            if (sacrifition == null) {
                return;
            }
            Devour(Sacrifition);
        }
        model.level++;
        model.eat.duration = model.die.duration = model.eat.spent;
        ResetTimers();
    }

    public void ResetTimers() {
        model.eat.start = Time.time;
        model.die.start = model.eat.finish;
    }

    public CreatureView Sacrifition => 
        GameManager.instance.worldView.creatureViews.FirstOrDefault(c => c.model.level == model.level - Creature.SACRIFITION_DELTA);

    public void Devour(CreatureView target) {
        Destroy(target.gameObject);
    }

    public void RandomizeLocation() {
        var width = Camera.main.aspect * Camera.main.orthographicSize * 2 - 4 * BaseRadius;
        var height = Camera.main.orthographicSize * 2 - 4 * BaseRadius;

        transform.position = new Vector2(
            Random.Range(-width / 2, width / 2),
            Random.Range(-height / 2, height / 2)
        );
    }

    public void RandomizeLocationAwayFromOthers() {
        float preferredDistance = MIN_DISTANCE_TO_OTHERS;

        for (int i = 0; i < 1000; i++) {
            RandomizeLocation();

            float minDistanceToOthers = GameManager.instance.worldView.creatureViews.Except(this).ExtMin(c => Vector2.Distance(transform.position, c.transform.position));
            if (minDistanceToOthers > preferredDistance) {
                break;
            }
            preferredDistance *= 0.9f; // no place found, allow place closer to others
        }
    }

    public void Born() {
        ResetTimers();
        RandomizeLocationAwayFromOthers();
    }

    public void OnDestroy() {
        if (GameManager.instance == null) {
            return;
        }
        GameManager.instance.worldView.creatureViews.Remove(this);
    }

    public void Die() {
        Destroy(gameObject);
        var explosion = Instantiate(explosionSample);
        explosion.transform.position = transform.position;
    }
}