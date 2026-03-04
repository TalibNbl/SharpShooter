using UnityEngine;
using StarterAssets;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;
    
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private Weapon currentWeapon;
    private float timeSinceLastShot = 0;
    const string SHOOT_STRING = "Shoot";

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {

        HandleShoot();
        timeSinceLastShot += Time.deltaTime;
    }

    private void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;
        
        if(timeSinceLastShot >= weaponSO.FireRate)
        {
        currentWeapon.Shoot(weaponSO);
        animator.Play(SHOOT_STRING, 0, 0f);
        timeSinceLastShot = 0;
        }

        if(!weaponSO.IsAutomatic)
        {
        starterAssetsInputs.ShootInput(false);
        }

    }

    public void SwitchWeapon(WeaponSO weapon)
    {
        Debug.Log(weapon.name);
    }
}
