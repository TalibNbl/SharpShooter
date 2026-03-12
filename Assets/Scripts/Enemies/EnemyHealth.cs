using UnityEngine;

public class EnemyHealth : MonoBehaviour
{   
    [SerializeField] private GameObject robotExplosionVFX;
    [SerializeField] private int healthAmount = 3;
    
    private GameManger gameManger;

    private void Start()
    {
        gameManger = FindFirstObjectByType<GameManger>();
        gameManger.AdjustEnemiesLeftText(1);
    }

    public void TakeDamage(int damage)
    {
        healthAmount = healthAmount - damage;

        if(healthAmount <= 0)
        {
            gameManger.AdjustEnemiesLeftText(-1);
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
