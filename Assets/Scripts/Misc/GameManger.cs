using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [SerializeField] private TMP_Text enemiesLeftText;
    [SerializeField] private GameObject youWinText;

    private int enemiesLeft = 0;
    const string ENEMIES_LEFT_STRING = "Enemies Left: ";

    public void AdjustEnemiesLeftText(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();
        if(enemiesLeft <= 0)
        {
            youWinText.SetActive(true);
        }
    }
    public void RestartLevelButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitButton()
    {
        Debug.LogWarning("Quit");
        Application.Quit();
    }
}
