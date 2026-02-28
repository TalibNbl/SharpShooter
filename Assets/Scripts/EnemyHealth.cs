using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int healthAmount = 3;

    public void TakeDamage(int damage)
    {
        healthAmount = healthAmount - damage;

        if(healthAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
