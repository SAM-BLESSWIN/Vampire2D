using UnityEngine;
using UnityEngine.UI;

public class LevelCompletionController : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletionPanel;
    [SerializeField] private Button btn_PlayNextLevel;
    [SerializeField] private Button btn_Exit;

    private void Awake()
    {
        btn_PlayNextLevel.onClick.AddListener(LoadNextLevel);
        btn_Exit.onClick.AddListener(QuitLevel);
    }

    private void Start()
    {
        levelCompletionPanel.SetActive(false);
    }

    public void LevelCompleted()
    {
        levelCompletionPanel.SetActive(true);
    }

    private void LoadNextLevel()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        SceneController.LoadNextScene();
    }

    private void QuitLevel()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        SceneController.LoadScene(0);
    }
}
