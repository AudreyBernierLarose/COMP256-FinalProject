using UnityEngine;

public class StartGameHandler : MonoBehaviour
{
    [SerializeField] private GameObject _introPanel;

    private void Awake()
    {
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            _introPanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
