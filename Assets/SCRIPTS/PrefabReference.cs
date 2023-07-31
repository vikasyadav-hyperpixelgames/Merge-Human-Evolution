using System.Collections.Generic;
using UnityEngine;

public class PrefabReference : MonoBehaviour
{
    private static PrefabReference instance;

    public static PrefabReference Instance
    {
        get { return instance; }
    }
    public List<GameObject> HumanImages;
    private void Awake()
    {
        // Check if an instance of the singleton already exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Set the instance and mark it as persistent across scenes
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
