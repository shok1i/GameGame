using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public uint seed;
    public int level;
    public float health;
}

public class PauseMenu : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject pauseMenuUI;
    public AudioSource audioSource;
    public GameObject player;
    public GameObject _dungeonManager;
    private GenerateDungeon _generateDungeon;
    private bool _isPaused = false;

    void Start()
    {
        _generateDungeon = _dungeonManager.GetComponent<GenerateDungeon>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        inGameUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        audioSource.UnPause();
        _isPaused = false;
    }
    public void Pause()
    {
        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        audioSource.Pause();
        _isPaused = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SaveGame()
    {
        uint dungeonSeed = _generateDungeon.getSeed();

        PlayerData data = new PlayerData
        {
            seed = dungeonSeed,
            level = 0,
            health = player.GetComponent<PlayerManager>().GetComponent<PlayerHealth>().getHealth()
        };

        string json = JsonUtility.ToJson(data, true);
        string time = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string folderPath = Application.persistentDataPath + "/savedGames";
        string path = Application.persistentDataPath + "/savedGames/playerData" + time + ".json";
        Debug.Log(folderPath);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        File.WriteAllText(path, json);
    }
}
