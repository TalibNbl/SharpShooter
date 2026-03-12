using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [Range(1, 10)]
    [SerializeField] private int healthAmount = 5;
    [SerializeField] private CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] private Transform weaponCamera;
    [SerializeField] private Image[] shiledBars;
    [SerializeField] private GameObject gameOverContainer;

    private int currentHealth;
    private int gameOverVirtualCameraPriority = 20;

    private void Awake()
    {
        currentHealth = healthAmount;
        AdjustSheldUI();
    }
    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        AdjustSheldUI();

        if(currentHealth <= 0)
        {
            PlayerGameOver();
        }
    }

    private void PlayerGameOver()
    {
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
        gameOverContainer.SetActive(true);
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false);
        Destroy(gameObject);
    }

    private void AdjustSheldUI()
    {
        for (int i = 0; i < shiledBars.Length; i++)
        {
            if (i < currentHealth)
            {
                shiledBars[i].gameObject.SetActive(true);
            }
            else
            {
                shiledBars[i].gameObject.SetActive(false);
            }
        }
    }
}
