using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject weaponContainer;

    private GenerateDungeon _generateDungeon;
    private bool _isPaused = false;
    private Vector3 gunPos;
    private Quaternion gunRot;
    private Vector3 playerScale;
    

    void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("musicVolume");
        _generateDungeon = _dungeonManager.GetComponent<GenerateDungeon>();
    }

    void Update()
    {
        if (_isPaused)
        {
            weaponContainer.transform.GetChild(0).transform.position = gunPos;
            weaponContainer.transform.GetChild(0).transform.rotation = gunRot;
            player.transform.localScale = playerScale;
        }
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
        playerScale = player.transform.localScale;
        gunRot = weaponContainer.transform.GetChild(0).transform.rotation;
        gunPos = weaponContainer.transform.GetChild(0).transform.position;
        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        audioSource.Pause();
        _isPaused = true;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SaveGame()
    {
        uint dungeonSeed = _generateDungeon.getSeed();

        PlayerData data = new PlayerData
        {
            seed = dungeonSeed,
            level = transform.parent.gameObject.GetComponentInChildren<RoomsManager>().getLevel(),
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
