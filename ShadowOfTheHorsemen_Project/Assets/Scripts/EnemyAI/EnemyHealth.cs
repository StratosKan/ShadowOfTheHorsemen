using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 3;
    public int currentHealth;
    public Slider healthBar;

    bool isDead;
	
	void Start () //I prefer Start over Awake for object pooling purposes. Not sure if correct logic.
    {
        currentHealth = startingHealth;
	}
	
	void Update ()
    {
        if(healthBar != null)
        {
            healthBar.transform.LookAt(Camera.main.transform.position);
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage (int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        this.gameObject.SetActive(false);
    }
}
