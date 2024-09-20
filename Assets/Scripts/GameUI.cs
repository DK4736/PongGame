using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    public ScoreText scoreTextPlayer1, scoreTextPlayer2;
    public GameObject menuObject;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI playModeButtonText;

    public System.Action onStartGame;

    private void Start()
    {
        
        AdjustPlayModeButtonText();
    }


     public void UpdateScores(int scorePlayer1, int scorePlayer2)
    {
        scoreTextPlayer1.SetScore(scorePlayer1);
        scoreTextPlayer2.SetScore(scorePlayer2);
    }

    public void HighlightScore(int id)
    {
        if (id ==1 )
            scoreTextPlayer1.Highlight();
        else   
            scoreTextPlayer2.Highlight();
    }

    public void OnStartGameButtonClicked()
    {
        menuObject.SetActive(false);
        onStartGame?.Invoke();
    }

    public void OnGameEnds(int winnerId)
    {
        menuObject.SetActive(true);
        winText.text = $"Player {winnerId} wins!";
    }

    
    private void AdjustPlayModeButtonText()
    {

        switch (GameManager.instance.playMode)
        {
            case GameManager.PlayMode.PlayerVsPlayer:
                 playModeButtonText.text = "2 Players";
                 break;
            case GameManager.PlayMode.PlayerVsAi:
                 playModeButtonText.text = "Player vs AI";
                 break;     
        }

    }

    public void OnSwitchPlayModeButton()
    {
       GameManager.instance.SwitchPlayMode();
       AdjustPlayModeButtonText();
    }
}
