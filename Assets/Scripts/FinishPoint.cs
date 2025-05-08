using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletePanel; // Panel "Level Complete"

    private void Start()
    {
        // Pastikan panel "Level Complete" tidak aktif saat permainan dimulai
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Level Complete Panel belum diatur di Inspector.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Hentikan waktu permainan
            Time.timeScale = 0f;

            // Tampilkan panel "Level Complete"
            if (levelCompletePanel != null)
            {
                levelCompletePanel.SetActive(true);
            }
            else
            {
                Debug.LogError("Level Complete Panel tidak ditemukan.");
            }

            // Hentikan timer speedrun jika ada
            SpeedrunTimer speedrunTimer = FindObjectOfType<SpeedrunTimer>();
            if (speedrunTimer != null)
            {
                speedrunTimer.StopTimer();
            }
        }
    }

    // Fungsi untuk melanjutkan ke level berikutnya
    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Lanjutkan waktu permainan
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Muat level berikutnya
        }
        else
        {
            Debug.Log("Tidak ada level selanjutnya!"); // Tampilkan pesan jika tidak ada level berikutnya
            LoadMainMenu(); // Kembali ke menu utama jika tidak ada level berikutnya
        }
    }

    // Fungsi untuk memulai ulang level saat ini
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Lanjutkan waktu permainan
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Muat ulang level saat ini
    }

    // Fungsi untuk kembali ke menu utama
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Lanjutkan waktu permainan
        SceneManager.LoadScene("MainMenu"); // Ganti dengan nama scene menu utama Anda
    }
}
