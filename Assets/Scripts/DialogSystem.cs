using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one");
            return;
        }
        instance = this;
    }
}
