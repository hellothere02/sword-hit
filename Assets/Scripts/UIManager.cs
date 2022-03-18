using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject swordStart;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private Text[] helmetsCountText;
    [SerializeField] private Text maxLevelText;
    [SerializeField] private Text currentLevelText;
    [SerializeField] private Text swordsCount;

    private int helmetsCount;
    private int maxLevel = 0;
    private static bool isPlaying;
    public static bool IsPlaying
    {
        get => isPlaying;
        set => isPlaying = value;
    }

    private void Awake()
    {
        LoadGame();
        Time.timeScale = 0;
        startCanvas.SetActive(true);
    }

    private void Update()
    {
        if (isPlaying)
        {
            string swords = SwordShot.SwordsCounts();
            swordsCount.text = swords;
        }

        maxLevelText.text = "Max Level: " + maxLevel.ToString();

        helmetsCount = Sword._helmetCount;
        foreach (var item in helmetsCountText)
        {
            item.text = helmetsCount.ToString();
        }

        if (Sword._gameIsOver)
        {
            GameOver();
        }
    }

    public void StartGame()
    {

        Time.timeScale = 1;
        startCanvas.SetActive(false);
        swordStart.SetActive(true);
        gameCanvas.SetActive(true);
        endCanvas.SetActive(false);
        isPlaying = true;
    }

    public void Restart()
    {
        Sword._gameIsOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        SaveGame();
        isPlaying = false;
        Time.timeScale = 0;
        gameCanvas.SetActive(false);
        endCanvas.SetActive(true);
        currentLevelText.text = "Current level: " + SwordShot.CurrentLevel.ToString();
        if (SwordShot.CurrentLevel > maxLevel)
        {
            maxLevel = SwordShot.CurrentLevel;
        }
        SwordShot.CurrentLevel = 0;
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("Score", helmetsCount);
        PlayerPrefs.SetInt("MaxLevel", maxLevel);
    }
    private void LoadGame()
    {
        helmetsCount = PlayerPrefs.GetInt("Score");
        maxLevel = PlayerPrefs.GetInt("MaxLevel");
    }

}
