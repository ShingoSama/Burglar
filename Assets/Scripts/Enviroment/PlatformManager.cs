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

    IEnumerator ReSpawnPlatforms(Vector2 position)
    {
        yield return new WaitForSeconds(3.5f);
        Instantiate(prefabPlatForm, position, prefabPlatForm.transform.rotation);
    }
}
