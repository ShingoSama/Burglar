using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance = null;
    public GameObject prefabPlatForm;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        
    }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReSpawn(Vector2 position, float timeToRespawn, float timeToDestroy)
    {
        StartCoroutine(ReSpawnPlatforms(position, timeToRespawn, timeToDestroy));
    }
    IEnumerator ReSpawnPlatforms(Vector2 position, float timeToRespawn, float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToRespawn);
        prefabPlatForm.gameObject.GetComponent<VanishPlatform>().timeToRespawn = timeToRespawn;
        prefabPlatForm.gameObject.GetComponent<VanishPlatform>().timeToDestroy = timeToDestroy;
        Instantiate(prefabPlatForm, position, prefabPlatForm.transform.rotation);
    }
}
