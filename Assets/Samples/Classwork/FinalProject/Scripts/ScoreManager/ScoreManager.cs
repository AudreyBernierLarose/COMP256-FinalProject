using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int _enemyScore = 0;
    private int _playerScore = 0;

    [SerializeField] private TextMeshProUGUI _playerScoreTextUI;
    [SerializeField] private TextMeshProUGUI _enemyScoreTextUI;

    // Update is called once per frame
    void Update()
    {
        _playerScoreTextUI.text = $"Player Score {_playerScore}";
        _enemyScoreTextUI.text = $"Enemy Score {_enemyScore}";


        //call the game over scene
        if (_enemyScore >= 3)
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
    public void IncrementEnemyScore() => ++_enemyScore;
}
