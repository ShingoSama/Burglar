using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public KnowPreviousScene previousScene;
    public string nextScene;
    public LevelLoader levelLoader;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (previousScene != null)
            {
                previousScene.BeforeChangeScene(SceneManager.GetActiveScene().name);
            }
            levelLoader.StartChangeScene(nextScene);
        }
    }
}
