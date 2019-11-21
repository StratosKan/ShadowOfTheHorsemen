using UnityEngine;

public class FireballDamageScript : MonoBehaviour
{
    //this is nested in the Fireball prefab and handles the doing damage to the player or enemy
    //also gives it a slight rotation
    public int damageToDeal = 2;

    private Rigidbody fireball_rb;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;

    void Awake()
    {
        fireball_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        fireball_rb.transform.Rotate(Vector3.up, 5f);
    }

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageToDeal);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageToDeal);
        }
        Destroy(gameObject);
    }
}