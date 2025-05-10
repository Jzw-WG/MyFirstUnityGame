using System.Collections;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowFactor = 0.5f;
    public float slowDuration = 1.5f;
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplySlow(slowFactor, slowDuration);
            }
        }
    }
}
