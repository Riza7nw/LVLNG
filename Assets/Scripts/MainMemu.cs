using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMemu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}
