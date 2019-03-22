using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScores : MonoBehaviour
{
    [SerializeField] private Image[] scoreImages;
    

    private GameManager gameManager;
    private GameObject[] sortedPlayers = new GameObject[4];
    private float[] sortedScores=new float[4];
    private Dictionary<GameObject, float> leftPlayers;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        leftPlayers = new Dictionary<GameObject, float>();
        for(int i=0;i<gameManager.GetPlayers().Length;i++)
        {
            leftPlayers.Add(gameManager.GetPlayers()[i], gameManager.GetScores()[i]);
        }

        SortScores();

        for(int i=0;i<scoreImages.Length;i++)
        {
            scoreImages[i].fillAmount = sortedScores[i] / gameManager.GetMaxPoints();
            scoreImages[i].color = gameManager.GetChosenColors()[i];
        }
    }

    private void SortScores()
    {
        for(int i=0;i<sortedPlayers.Length;i++)
        {
            sortedScores[i] = GetHightestScore(out sortedPlayers[i]);
            leftPlayers.Remove(sortedPlayers[i]);
        }

    }

    private float GetHightestScore(out GameObject playerManager)
    {
        float highestScore = 0f;
        playerManager = null;
        foreach(GameObject player in leftPlayers.Keys)
        {
            if (leftPlayers[player] > highestScore)
            {
                highestScore = leftPlayers[player];
                playerManager = player;
            }
        }

        return highestScore;
    }
}
