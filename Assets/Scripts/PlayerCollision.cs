using UnityEngine;
using System.Collections;
public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            HealtManager.health--; // Kurangi nyawa pemain
            if (HealtManager.health <= 0)
            {
                PlayerManager.isGameOver = true; // Tandai game over
                FindObjectOfType<SpeedrunTimer>().StopTimer(); // Hentikan timer
                gameObject.SetActive(false); // Nonaktifkan karakter
            }
            else
            {
                StartCoroutine(GetHurt()); // Jalankan animasi terluka
            }
        }
    }

    IEnumerator GetHurt()
    {
        // Abaikan tabrakan sementara antara layer 6 (Player) dan layer 8 (Enemy)
        Physics2D.IgnoreLayerCollision(6, 8);
        GetComponent<Animator>().SetLayerWeight(1, 1); // Aktifkan layer animasi terluka
        yield return new WaitForSeconds(3); // Tunggu 3 detik
        GetComponent<Animator>().SetLayerWeight(1, 0); // Nonaktifkan layer animasi terluka
        Physics2D.IgnoreLayerCollision(6, 8, false); // Pulihkan tabrakan
    }
}