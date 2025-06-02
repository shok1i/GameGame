using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    public float elapsedTime = 0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }
}
