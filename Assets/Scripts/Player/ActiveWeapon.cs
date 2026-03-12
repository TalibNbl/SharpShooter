using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO startingWeapon;
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] private Camera weaponCamera;
    [SerializeField] private GameObject zoomVignette;
    [SerializeField] private TMP_Text ammoText;

    private WeaponSO currentWeaponSO;
    private StarterAssetsInputs starterAssetsInputs;
    private FirstPersonController firstPersonController;
    private Animator animator;
    private Weapon currentWeapon;
    private float timeSinceLastShot = 0;
    private float defaultFOV;
    private float defaultRotationSpeed;
    private int currentAmmo;
    const string SHOOT_STRING = "Shoot";

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
        currentWeaponSO = startingWeapon;
    }

    private void Start()
    {
        AdjustAmmo(currentWeaponSO.MagazineSize);
        SwitchWeapon(startingWeapon);
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;

        if(currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }

        ammoText.text = currentAmmo.ToString("D2");
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

       Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
       currentWeapon = newWeapon;
       this.currentWeaponSO = weaponSO;
       AdjustAmmo(weaponSO.MagazineSize);
    }
    private void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (!starterAssetsInputs.shoot) return;
        
        if(timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
        currentWeapon.Shoot(currentWeaponSO);
        animator.Play(SHOOT_STRING, 0, 0f);
        timeSinceLastShot = 0;
        AdjustAmmo(-1);
        }

        if(!currentWeaponSO.IsAutomatic)
        {
        starterAssetsInputs.ShootInput(false);
        }

    }

    private void HandleZoom()
    {
        if(!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
            
            
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
            zoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }
}
