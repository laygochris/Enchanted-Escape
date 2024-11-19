using UnityEngine;

public class FireMeteor : MonoBehaviour
{
    [SerializeField] private Vector2 dropSpeed ;
    [SerializeField] private float destroyTime = 10f;
    [SerializeField] AudioClip bulletHit;
    [SerializeField] AudioSource AS;
    [SerializeField] GameObject impactEffect;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float x = Random.Range(dropSpeed.x,dropSpeed.y);
        rb.velocity = new Vector2(-x, -x);
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(5);

                Instantiate(impactEffect, transform.position, transform.rotation);
                AS.PlayOneShot(bulletHit);
            }
            DestroyMeteor();
        }
    }

    void DestroyMeteor()
    {
        MeteorSpawner spawner = FindObjectOfType<MeteorSpawner>();
        spawner.MeteorDestroyed(); // Notify the spawner that this meteor was destroyed
        Destroy(gameObject);
    }
}
