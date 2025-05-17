using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    [SerializeField]
    private GameObject gameOverScreen;

    void Awake()
    {
        // Pastikan hanya ada satu PlayerManager yang bertahan antar scene
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Saat scene dimulai, pastikan layar game over dimatikan
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    void Update()
    {
        if (isGameOver && gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void ReplayLevel()
    {
        isGameOver = false;

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
