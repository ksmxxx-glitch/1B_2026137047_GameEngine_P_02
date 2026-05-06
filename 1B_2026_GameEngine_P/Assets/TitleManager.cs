using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class TitleManager : MonoBehaviour

{
    public GameObject helpPanel;
    public GameObject leaderboard;

    public void GameStart()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }

    public void OpenLeaderboard()
    {
        leaderboard.SetActive(true);
    }



    public void GameTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
