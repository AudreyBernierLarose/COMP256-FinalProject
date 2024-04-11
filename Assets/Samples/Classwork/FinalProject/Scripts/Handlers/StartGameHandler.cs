using UnityEngine;

public class StartGameHandler : MonoBehaviour
{
    [SerializeField] private GameObject _introPanel;
    [SerializeField] private PlayerController _playerControls;
    [SerializeField] private AgentShooter _enemyGhost;

    private void Awake()
    {
        Time.timeScale = 0.0f;
        _playerControls.enabled = false; //make sure the player can't shoot to start
        _enemyGhost.enabled = false; //make sure the ghost can't shoot to start
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            _introPanel.SetActive(false);
            Time.timeScale = 1.0f;
            _playerControls.enabled = true; //make sure the player can shoot once the game starts
            _enemyGhost.enabled = true; //make sure the enemy can shoot once the game starts
        }
    }
}
