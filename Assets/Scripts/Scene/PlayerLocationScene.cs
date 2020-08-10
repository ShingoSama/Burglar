using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class PlayerLocationScene : MonoBehaviour
{
    public PlayerController playerController;
    public PlayableDirector playerStart;
    public PlayableDirector PlayerBosque2;
    public void SetLocation(string lastSceneLocation)
    {
        if(lastSceneLocation == "Level2")
        {
            playerController.transform.position = new Vector2 (88f, 0.32f);
            PlayerBosque2.Play();
        }
        if (lastSceneLocation == "")
        {
            playerController.transform.position = new Vector2(-17.16f, 2.39f);
            playerStart.Play();
        }
    }
}
