using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public KnowPreviousScene previousScene;
    public string nextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            previousScene.BeforeChangeScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(nextScene);
        }
    }
}
