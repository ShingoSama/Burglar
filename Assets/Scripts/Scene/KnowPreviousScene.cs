using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowPreviousScene : MonoBehaviour
{
    public static KnowPreviousScene knowPreviousScene;
    [SerializeField]
    private PlayerLocationScene PlayerLocationScene;
    private string previousNameScene;
    void Awake()
    {
    }
    private void Start()
    {
        if (knowPreviousScene == null)
        {
            PlayerPrefs.SetString("LastScene", "");
            knowPreviousScene = this;
            DontDestroyOnLoad(this);
        }
        knowPreviousScene.previousNameScene = PlayerPrefs.GetString("LastScene");
        if (PlayerLocationScene != null)
        {
            PlayerLocationScene.SetLocation(knowPreviousScene.previousNameScene);
        }
    }
    public void BeforeChangeScene(string name)
    {
        PlayerPrefs.SetString("LastScene", name);
    }
}
