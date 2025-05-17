using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;

    void Awake()
    {
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

    }

    void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
   