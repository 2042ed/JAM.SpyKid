using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HomeManager : MonoBehaviour
{
    static HomeManager _instance;

    public static HomeManager Instance {
        get {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    public void OpenSceneCrypto()
    {
        SceneManager.LoadScene("crypto");
    }
}
