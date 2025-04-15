using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minhealt : MonoBehaviour
{
    void Update()
    {
        // Tampilkan jumlah nyawa di konsol (atau gunakan UI untuk menampilkan nyawa)
        Debug.Log($"Lives Remaining: {PlayerStats.Instance.Lives}");
    }
}
