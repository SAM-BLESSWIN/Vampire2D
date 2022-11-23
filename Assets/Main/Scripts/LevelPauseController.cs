using UnityEngine;
using UnityEngine.UI;

public class LevelPauseController : MonoBehaviour
{
    [SerializeField] private GameObject levelPausePanel;
    [SerializeField] private Button btn_QuitLevel;
    [SerializeField] private Button btn_Resume;

    private void Awake()
    {
        btn_QuitLevel.onClick.AddListener(QuitLevel);
        btn_Resume.onClick.AddListener(ResumeLevel);
    }

    private void Start()
    {
        levelPausePanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseLevel();
        }
    }
    private void PauseLevel()
    {
        Time.timeScale = 0f;
        levelPausePanel.SetActive(true);
    }

    private void ResumeLevel()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        Time.timeScale = 1f;
        levelPausePanel.SetActive(false);
    }

    private void QuitLevel()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        SceneController.LoadScene(0);
    }
}
