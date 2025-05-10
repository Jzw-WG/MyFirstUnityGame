using UnityEngine;

public class WaterBubbleProjectile : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float lifetime = 10f;
    public GameObject slowZonePrefab;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Explode();
        }

        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Debug.Log(direction);
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Weapon Item"))
        {
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(slowZonePrefab, transform.position, Quaternion.identity).SetActive(true);
        Destroy(gameObject);
    }
}
