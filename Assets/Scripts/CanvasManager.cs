using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private Text gameWonText;

    [SerializeField]
    private Text gameLostText;

    private void Awake()
    {
        GameController.GameLost += this.DisplayGameLostText;
        GameController.GameWon += this.DisplayGameWonText;
        GameController.StartGame += this.ResetUi;
    }

    private void DisplayGameWonText()
    {
        this.gameWonText.gameObject.SetActive(true);
    }

    private void DisplayGameLostText()
    {
        this.gameLostText.gameObject.SetActive(true);
    }

    private void ResetUi()
    {
        this.gameWonText.gameObject.SetActive(false);
        this.gameLostText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameController.GameLost -= this.DisplayGameLostText;
        GameController.GameWon -= this.DisplayGameWonText;
        GameController.StartGame -= this.ResetUi;
    }
}
