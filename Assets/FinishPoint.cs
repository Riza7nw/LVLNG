using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] bool goToNextLevel;
    [SerializeField] string LevelName;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           if(goToNextLevel)
            {
                SceneController.instance.NextLevel1();
            }
            else
            {
                SceneController.instance.LoadScene(LevelName);
            }

        }
    }
}
