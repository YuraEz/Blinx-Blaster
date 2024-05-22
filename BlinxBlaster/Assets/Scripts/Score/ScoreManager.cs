using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreInGameText;
    public int score;
    public int scoreInGame;
    public int scoreInGameBfUpt;

    private bool checked250 = false;
    private bool checked350 = false;


    //  private void Start()
    //   {


    // if (scoreInGameText) scoreInGameText.SetText($"{scoreInGame}");
    //  }

    private void OnEnable()
    {
        score = PlayerPrefs.GetInt("score", 0);
        scoreText.SetText($"{score}");
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void UpdateScore(int value)
    {
        score -= value;
        PlayerPrefs.SetInt("score", score);
    }

    public void UpdateGame(int value)
    {
        scoreInGame += value;

        if (scoreInGame > 250 && !checked250)
        {
            PlayerPrefs.SetInt("goalamount3", PlayerPrefs.GetInt("goalamount3", 0) + 1);
            checked250 = true; // Помечаем, что 250 очков достигнуты в этом матче
        }

        if (scoreInGame > 350 && !checked350)
        {
            PlayerPrefs.SetInt("goalamount4", PlayerPrefs.GetInt("goalamount4", 0) + 1);
            checked350 = true; // Помечаем, что 350 очков достигнуты в этом матче
        }

        scoreText.SetText($"{score+scoreInGame}");
        PlayerPrefs.SetInt("score", score + scoreInGame);
        //  if (scoreInGameText) scoreInGameText.SetText($"{scoreInGame}");
    }

    public void Finish(bool win = false)
    {
        scoreInGameBfUpt = scoreInGame;
        if (win)
        {
            
            scoreInGame += scoreInGame/6;
        }
        PlayerPrefs.SetInt("score", score + scoreInGame);
    }

    public void increaseValue(int value)
    {
        PlayerPrefs.SetInt("score", score + value);
        score += value;
        scoreText.SetText($"{PlayerPrefs.GetInt("score", 0)}");
    }
}
