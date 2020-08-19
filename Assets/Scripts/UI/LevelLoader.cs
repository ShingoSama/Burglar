using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void StartChangeScene(int numeroesena)
    {
        StartCoroutine(LoadLevel(numeroesena));
    }
    public void StartChangeScene(string nombrescena)
    {
        StartCoroutine(LoadLevel(nombrescena));
    }
    IEnumerator LoadLevel(int numeroesena)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(numeroesena);
    }
    IEnumerator LoadLevel(string nombrescena)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nombrescena);
    }
}
