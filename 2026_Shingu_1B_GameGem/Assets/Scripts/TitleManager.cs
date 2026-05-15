using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject helpPannel;
    public GameObject settingPannel;

    public void StartButton()
    {
        Debug.Log("StartButton Clicked");
        SceneManager.LoadScene("IceGameScene_1");
    }


    public void OpenHelp()
    {
        helpPannel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPannel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingPannel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingPannel.SetActive(false);
    }

    public void OpenTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }

    private void Update()
    {
    }
}
