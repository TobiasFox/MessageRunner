using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChooser : MonoBehaviour
{
    private static ArrayList colorList;
    private static ArrayList choosenNumbers = ArrayList.Synchronized(new ArrayList());
    private static List<PlayerChooser> playerChoosers = new List<PlayerChooser>();

    private Image playerImage;
    private string xAxis;
    private string fire1;
    private int currentColorIndex = 0;
    [SerializeField] [Range(0, 3)] private int playerNumber = 0;
    [SerializeField] private CustomColors definedColors;
    private bool isChangingColor = false;
    [SerializeField] private float timeInterval = 0.4f;
    private float readyForNextInput = 0;
    private Text playerText;
    private bool isReady = false;
    private GameManager gameManager;
    private SceneLoader loader;

    void Start()
    {
        playerImage = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
        loader = FindObjectOfType<SceneLoader>();
        playerText = transform.GetChild(1)?.GetComponent<Text>();
        xAxis = "Horizontal_P" + playerNumber;
        fire1 = "Fire1_P" + playerNumber;

        currentColorIndex = playerNumber;
        playerImage.color = definedColors.colors[currentColorIndex];
        playerChoosers.Add(this);

        if (colorList == null)
        {
            colorList = ArrayList.Synchronized(new ArrayList());
            for (int i = 0; i < definedColors.colors.Length; i++)
            {
                colorList.Add(definedColors.colors[i]);
            }

            loader.StartAsyncSceneLoading("CarinaScene");
        }

    }

    void Update()
    {
        if (isReady)
        {
            return;
        }

        if (isChangingColor)
        {
            if (Time.time > readyForNextInput)
            {
                isChangingColor = false;
            }
        }
        else
        {
            PreviewNextColor();
        }

        if (Input.GetButtonDown(fire1))
        {
            submitColor();
        }

    }

    private void submitColor()
    {
        isReady = true;
        colorList.RemoveAt((int)((CustomColors.Colors)currentColorIndex));
        playerText.text += "\nReady";
        gameManager.SetPlayer(playerNumber, definedColors.colors[currentColorIndex]);

        if (choosenNumbers.Count >= 4)
        {
            loader.ShowLoadedScene();
            return;
        }

        playerChoosers.Remove(this);
        foreach (var chooser in playerChoosers)
        {
            chooser.UpdateChooseSelection(currentColorIndex);
        }
    }

    private void UpdateChooseSelection(int selectedIndex)
    {
        if (selectedIndex == currentColorIndex)
        {
            currentColorIndex = Random.Range(0, colorList.Count);
            playerImage.color = (Color)colorList[currentColorIndex];
        }
    }

    private void PreviewNextColor()
    {
        var moveLeftRight = Input.GetAxis(xAxis);

        if (moveLeftRight > 0)
        {
            currentColorIndex = (currentColorIndex + 1) % colorList.Count;
            playerImage.color = (Color)colorList[currentColorIndex];
            isChangingColor = true;
            readyForNextInput = Time.time + timeInterval;
        }
        else if (moveLeftRight < 0)
        {

            currentColorIndex = currentColorIndex == 0 ? currentColorIndex = colorList.Count - 1 : currentColorIndex - 1;
            playerImage.color = (Color)colorList[currentColorIndex];
            readyForNextInput = Time.time + timeInterval;
            isChangingColor = true;
        }
    }
}
