using UnityEngine;
using UnityEngine.UI;

public class GameoverController : MonoBehaviour
{
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private Button btn_Restart;
    [SerializeField] private Button btn_Exit;

    private void Awake()
    {
        btn_Restart.onClick.AddListener(RestartGame);
        btn_Exit.onClick.AddListener(QuitLevel);
    }

    private void Start()
    {
        gameoverPanel.SetActive(false); 
    }

    public void GameOver()
    {
        gameoverPanel.SetActive(true);
    }

    private void RestartGame()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        SceneController.LoadCurrentScene();
    }

    private void QuitLevel()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        SceneController.LoadScene(0);
    }
}
