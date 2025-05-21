using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCore : MonoBehaviour
{
    public GameObject mainBtnBg;
    
    private Vector3 _changePos = Vector3.zero;
    private bool _enterFlag = false;
    private bool _inSettings = false;
    private Animator _animator;
    
    void Start()
    {
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
}