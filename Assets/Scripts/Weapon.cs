using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;
    
    public void Shoot(WeaponSO weaponSO)
    {

        RaycastHit hit;
        muzzleFlash.Play();

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
        }

    }
}
