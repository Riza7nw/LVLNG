using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathRespawn : MonoBehaviour
{
    public bool hasDied;
    private Vector3 startPosition; // Posisi awal pemain

    void Start()
    {
        hasDied = false;
        startPosition = transform.position; // Simpan posisi awal pemain
    }

    void Update()
    {
        // Jika pemain jatuh di bawah ketinggian tertentu
        if (transform.position.y < -20)
        {
            hasDied = true;
        }

        // Jika pemain mati, respawn ke posisi awal
        if (hasDied)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = startPosition; // Kembalikan pemain ke posisi awal
        hasDied = false; // Reset status kematian
    }
}

