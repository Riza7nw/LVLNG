using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public Transform respawnPoint; // Tempat awal untuk respawn
    private HealthManager healthManager;

    private void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike")) // Deteksi jika terkena spike
        {
            healthManager.ReduceHealth(1); // Kurangi nyawa pemain
            RespawnPlayer(); // Respawn pemain
        }
    }

    private void RespawnPlayer()
    {
        transform.position = respawnPoint.position; // Reset posisi pemain ke tempat awal
    }
}
