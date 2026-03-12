using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject projectileHitVFXPrefab;
    
    private int damage;
    private Rigidbody projectileRB;

    private void Awake()
    {
        projectileRB = GetComponent<Rigidbody>();
    }
    private void Start() 
    {
        projectileRB.linearVelocity = transform.forward * speed;
    }

    public void Init(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
        playerHealth?.TakeDamage(damage);
        Debug.Log("damage given");

        Instantiate(projectileHitVFXPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }   
}
