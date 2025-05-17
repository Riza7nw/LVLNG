using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonFunctions : MonoBehaviour
{
    public void RestartLevel()
    {
        Debug.Log("TOMBOL RESTART DIKLIK!");
        Time.timeScale = 1f; // Pastikan waktu berjalan normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Debug.Log("TOMBOL NEXT LEVEL DIKLIK!");
        Time.timeScale = 1f; // Pastikan waktu berjalan normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
