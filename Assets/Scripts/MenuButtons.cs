using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject highScorePanel;

    public Texture2D cursor;
    public int highestStage;
    public TextMeshProUGUI highestStageText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighestStage"))
        {
            highestStage = PlayerPrefs.GetInt("HighestStage");
            highestStageText.text = highestStage.ToString();
        }
        else
        {
            highestStageText.text = "0";
        }
        Vector2 cursorCenter = new Vector2(cursor.width / 2, cursor.height / 2);
        Cursor.SetCursor(cursor, cursorCenter, CursorMode.Auto);
    }
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void HSButtonPressed()
    {
        mainPanel.SetActive(false);
        highScorePanel.SetActive(true);
    }

    public void BackButtonPressed()
    {
        mainPanel.SetActive(true);
        highScorePanel.SetActive(false);
    }
}
