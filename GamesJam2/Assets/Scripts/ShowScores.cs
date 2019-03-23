using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowScores : MonoBehaviour
{
    [SerializeField] private Image[] scoreImages;
    [SerializeField] private CustomColors customColors;
    [SerializeField] private Text text;

    private GameManager gameManager;
    private int[] sortedPlayerColors = new int[4];
    private float[] sortedScores = new float[4];
    private ArrayList leftPlayers;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        leftPlayers = new ArrayList();
        for (int i = 0; i < sortedPlayerColors.Length; i++)
        {
            leftPlayers.Add(i);
        }

        SortScores();
        List<string> winList = new List<string>();

        for (int i = 0; i < scoreImages.Length; i++)
        {
            scoreImages[i].fillAmount = sortedScores[i] / gameManager.GetMaxPoints();
            scoreImages[i].color = customColors.colors[sortedPlayerColors[i]];
            if (sortedScores[i] >= sortedScores[0])
            {
                string colorString = ((CustomColors.Colors)sortedPlayerColors[i]).ToString();
                winList.Add(colorString.Substring(0, 1) + colorString.Substring(1, colorString.Length - 1).ToLower());
            }
        }

        text.text = string.Join(", ", winList) + " win" + (winList.Count >= 2 ? "s" : "") + "!";
    }

    private void SortScores()
    {
        for (int i = 0; i < sortedPlayerColors.Length; i++)
        {
            sortedScores[i] = GetHightestScore(out sortedPlayerColors[i]);
            leftPlayers.Remove(sortedPlayerColors[i]);
        }

    }

    private float GetHightestScore(out int playerColor)
    {
        float highestScore = 0f;
        playerColor = (int)leftPlayers[0];
        foreach (int player in leftPlayers)
        {
            if (gameManager.GetScores()[player] > highestScore)
            {
                highestScore = gameManager.GetScores()[player];
                playerColor = player;
            }
        }

        return highestScore;
    }

}
