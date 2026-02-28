using UnityEngine;
using StarterAssets;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject hitVFXPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private int distance;
    [SerializeField] private int damageAmount = 1;

    private StarterAssetsInputs starterAssetsInputs;
    const string SHOOT_STRING = "Shoot";

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

    private void Update()
    {
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        muzzleFlash.Play();
        animator.Play(SHOOT_STRING, 0, 0f);
        starterAssetsInputs.ShootInput(false);

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(damageAmount);
            Instantiate(hitVFXPrefab, hit.point, Quaternion.identity);
        }

    }
}
