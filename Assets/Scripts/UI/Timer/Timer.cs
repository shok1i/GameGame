using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    
    public float elapsedTime = 0f;
    public TextMeshProUGUI text;

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

    void Start()
    {
        if (PlayerPrefs.GetInt("Timer") == 0)
        {
            text.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {

        }
        else if (PlayerPrefs.GetInt("Timer") == 1)
        {
            text.text = TimeSpan.FromSeconds((int)elapsedTime).ToString();
            
            elapsedTime += Time.deltaTime;
        }
    }
}
