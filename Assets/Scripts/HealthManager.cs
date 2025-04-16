using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int currentHealth = 3; // Nyawa awal pemain

    public void ReduceHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Game Over!"); // Tambahkan logika game over jika diperlukan
        }
    }
}
