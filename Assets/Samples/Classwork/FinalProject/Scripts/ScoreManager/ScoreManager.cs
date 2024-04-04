using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int _ghostScore = 0;
    private int _playerScore = 0;

    [SerializeField] private TextMeshProUGUI _playerScoreTextUI;
    [SerializeField] private TextMeshProUGUI _ghostScoreTextUI;

    // Update is called once per frame
    void Update()
    {
        _playerScoreTextUI.text = $"Player Score {_playerScore}";
        _ghostScoreTextUI.text = $"Ghost Score {_ghostScore}";


        //call the game over scene
        if (_ghostScore >= 3)
        {
            SceneManager.LoadScene("GameOver");
        }

        //call the win scene
        if(_playerScore >= 3)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    //methods to increment the scores properly
    public void IncrementPlayerScore() => ++_playerScore;
    public void IncrementEnemyScore() => ++_ghostScore;
}
