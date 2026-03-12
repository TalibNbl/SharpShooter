using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform turretHead;
    [SerializeField] private Transform playerTargetPoint;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float fireInterval = 3f;
    [SerializeField] private int damage = 2;

    PlayerHealth player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(FireRoutine());
    }
    private void Update()
    {
        turretHead.LookAt(playerTargetPoint);
    }

    private IEnumerator FireRoutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(fireInterval);
            Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.transform.LookAt(playerTargetPoint);
            newProjectile.Init(damage);
        }
    }
}
