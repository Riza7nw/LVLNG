using UnityEngine;
using TMPro;

public class SpeedrunTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestTimeText; // UI untuk menampilkan waktu terbaik

    private float elapsedTime = 0f;
    private bool isRunning = false; // Timer berjalan setelah pemain mulai bergerak
    private bool hasStarted = false; // Apakah pemain sudah mulai bergerak

    private float bestTime = 0f;

    private Rigidbody2D playerRigidbody; // Referensi ke Rigidbody2D pemain
    private const float movementThreshold = 0.1f; // Ambang batas untuk mendeteksi gerakan

    void Start()
    {
        // Ambil referensi ke Rigidbody2D pemain
        playerRigidbody = FindObjectOfType<Player>().GetComponent<Rigidbody2D>();
        if (playerRigidbody == null)
        {
            Debug.LogError("Rigidbody2D pemain tidak ditemukan. Pastikan pemain memiliki komponen Rigidbody2D.");
        }

        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        UpdateBestTimeUI();
    }

    void Update()
    {
        // Periksa apakah pemain mulai bergerak
        if (!hasStarted && playerRigidbody != null && Mathf.Abs(playerRigidbody.velocity.magnitude) > movementThreshold)
        {
            hasStarted = true; // Tandai bahwa pemain telah mulai bergerak
            isRunning = true;  // Mulai timer
        }

        // Jika timer berjalan, tambahkan waktu
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

        timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }

    void UpdateBestTimeUI()
    {
        if (bestTime > 0f)
        {
            int minutes = Mathf.FloorToInt(bestTime / 60f);
            int seconds = Mathf.FloorToInt(bestTime % 60f);
            int milliseconds = Mathf.FloorToInt((bestTime * 1000) % 1000);

            bestTimeText.text = $"Best: {minutes:00}:{seconds:00}:{milliseconds:000}";
        }
        else
        {
            bestTimeText.text = "Best: --:--:---";
        }
    }

    public void StopTimer()
    {
        isRunning = false;

        // Simpan waktu terbaik jika lebih cepat
        if (bestTime == 0f || elapsedTime < bestTime)
        {
            bestTime = elapsedTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
            UpdateBestTimeUI();
        }
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = false;
        hasStarted = false; // Reset status mulai
        UpdateTimerUI();
    }

    public void ResetBestTime()
    {
        PlayerPrefs.DeleteKey("BestTime");
        bestTime = 0f;
        UpdateBestTimeUI();
    }
}
