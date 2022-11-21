using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button btn_Play;
    [SerializeField] private Button btn_Quit;
    [SerializeField] private Button btn_Close;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelPanel;

    private void Awake()
    {
        btn_Quit.onClick.AddListener(Quit);
        btn_Play.onClick.AddListener(OpenLevelPanel);
        btn_Close.onClick.AddListener(CloseLevelPanel);
    }

    private void Quit()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        Application.Quit();
    }

    private void OpenLevelPanel()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        levelPanel.SetActive(true);
        mainMenu.SetActive(false);
    }

    private void CloseLevelPanel()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        levelPanel.SetActive(false);
        mainMenu.SetActive(true);
    }
}
