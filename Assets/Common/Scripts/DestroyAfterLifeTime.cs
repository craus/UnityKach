using TMPro;
using UnityEngine;


public class DestroyAfterLifeTime : MonoBehaviour
{
    public float lifeTime = 10;
    public float startTime;

    public void Awake() {
        startTime = Time.time;
    }

    public void Update() {
        if (Time.time > startTime + lifeTime) {
            Destroy(gameObject);
        }
    }
}
