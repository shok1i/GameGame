using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerFileData
{
    public uint seed;
    public int level;
    public float health;
}



public class MainMenuCore : MonoBehaviour
{
    public GameObject mainBtnBg;
    public GameObject savedDataPrefab;
    public GameObject scrollViewContainer;
    public GameObject savedData;
    public Slider volumeSlider;
    public Toggle timerToggle;
    public AudioSource audio;
    private Vector3 _changePos = Vector3.zero;
    private bool _enterFlag = false;
    private bool _inSettings = false;
    private Animator _animator;
    private bool timerStatus = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
            volumeSlider.value = 0.5f;
        }
        else
        {
            float volume = PlayerPrefs.GetFloat("musicVolume");
            audio.volume = volume;
            volumeSlider.value = volume;
        }
        readFiles();
        _animator = gameObject.GetComponent<Animator>();
    }

    // Animation Section
    public void GameStartAnimEnd()
    {
        _enterFlag = true;
    }

    public void SettingsEnterAnimEnd()
    {
        _inSettings = true;
        _animator.SetBool("SettingsEnter", false);
    }

    public void SettingsExitAnimEnd()
    {
        _inSettings = false;
        _animator.SetBool("SettingsExit", false);
    }

    // For LerpAnimation
    void Update()
    {
        LerpAnimation();
        SettingsExitHandle();
        if (Input.GetKey(KeyCode.Escape))
        {
            savedData.SetActive(false);
        }
    }

    void LerpAnimation()
    {
        if (!mainBtnBg.activeInHierarchy) return;

        mainBtnBg.transform.localPosition =
            Vector3.Lerp(mainBtnBg.transform.localPosition, _changePos, Time.deltaTime * 10);
    }

    void SettingsExitHandle()
    {
        if (!_inSettings) return;

        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        _animator.SetBool("SettingsExit", true);
        
    }


    // Enter Function
    private void EnterHandler(int num)
    {
        mainBtnBg.SetActive(true);
        switch (num)
        {
            case 0:
                _changePos = new Vector3(115f, 27.5f, 0f);
                break;
            case 1:
                _changePos = new Vector3(115f, -7.5f, 0f);
                break;
            case 2:
                _changePos = new Vector3(115f, -42.5f, 0f);
                break;
            default:
                Debug.Log($"We Fucked {num}");
                mainBtnBg.SetActive(false);
                break;
        }
    }

    // NewGame Section
    public void NewGame_Enter()
    {
        if (!_enterFlag) return;
        EnterHandler(0);
    }

    public void NewGame_Click()
    {
        if (!_enterFlag) return;
        SceneManager.LoadScene("SceneForRooms");
    }

    // Continue Section
    public void Continue_Enter()
    {
        if (!_enterFlag) return;
        EnterHandler(1);
    }

    public void Continue_Click()
    {
        if (!_enterFlag) return;
        _animator.SetTrigger("ContinueEnter");
    }

    // Settings Section
    public void Settings_Enter()
    {
        if (!_enterFlag) return;
        EnterHandler(2);
    }

    public void Settings_Click()
    {
        if (!_enterFlag) return;
        _animator.SetTrigger("SettingsEnter");
    }
    public void savedData_Click()
    {
        savedData.SetActive(true);
    }
    public void UpdateTimerStatus()
    {
        timerStatus = timerToggle.isOn;
        Debug.Log(timerStatus);
        PlayerPrefs.SetInt("Timer", timerStatus ? 1 : 0);
    }
    public void ChangeSoundValue()
    {
        audio.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    private void readFiles()
    {
        string folderPath = Application.persistentDataPath + "/savedGames";
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, "*.json");
            foreach (string filePath in files)
            {
                string json = System.IO.File.ReadAllText(filePath);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                var savedData = Instantiate(savedDataPrefab, parent: scrollViewContainer.transform); ;
                TextMeshProUGUI[] texts = savedData.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = texts[0].text.ToString().Replace("\\", "").Replace("TIME", filePath.ToString().Replace("playerData", "").Replace(".json", "").Replace("_", " ").Replace(folderPath, ""));
                texts[1].text = texts[1].text.ToString().Replace("SEED", data.seed.ToString());
                texts[2].text = texts[2].text.ToString().Replace("LEVEL", data.level.ToString());
                texts[3].text = texts[3].text.ToString().Replace("FILENAME", filePath);
                Debug.Log(savedData.GetComponent<Button>());
                savedData.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Debug.Log(savedData.GetComponent<Button>());
                    loadSave((int)data.seed, data.level, data.health);
                });
                Debug.Log(texts[3].text.Replace(folderPath, "").Replace("\\", ""));

            }
        }
        else
        {
            Directory.CreateDirectory(folderPath);
            // дополнить текст
            return;
        }
    }
    
    private void loadSave(int seed, int level, float health)
    {
        PlayerPrefs.SetFloat("Health", health);
        PlayerPrefs.SetInt("Seed", seed);
        PlayerPrefs.SetInt("Level", level);
        SceneManager.LoadScene("SceneForRooms");
    }
    
}