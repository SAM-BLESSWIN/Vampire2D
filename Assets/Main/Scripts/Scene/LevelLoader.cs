using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private Button btn_loadLevel;
    [SerializeField] private Image img_levelstatus;
    [SerializeField] private string levelName;

    [Header("level status")]
    [SerializeField] private Sprite locked;
    [SerializeField] private Sprite unLocked;
    [SerializeField] private Sprite completed;
    
    private void Awake()
    {
        btn_loadLevel = GetComponent<Button>(); 
    }

    private void Start()
    {
        SetLevelStatusUI();
        btn_loadLevel.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName);

        switch (levelStatus)
        {
            case LevelStatus.LOCKED:
                SoundManager.Instance.Play(SoundTypes.LEVELLOCKED);
                Debug.Log("[LEVEL LOCKED] please complete previous level to unlock!");
                break;
            case LevelStatus.UNLOCKED:
                SoundManager.Instance.Play(SoundTypes.LEVELLOAD);
                SceneController.LoadScene(levelName);
                break;
            case LevelStatus.COMPLETED:
                SoundManager.Instance.Play(SoundTypes.LEVELLOAD);
                SceneController.LoadScene(levelName);
                break;
        }
    }

    private void SetLevelStatusUI()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName);

        switch (levelStatus)
        {
            case LevelStatus.LOCKED:
                img_levelstatus.sprite = locked;
                break;
            case LevelStatus.UNLOCKED:
                img_levelstatus.sprite = unLocked;
                break;
            case LevelStatus.COMPLETED:
                img_levelstatus.sprite = completed;
                break;
        }
    }
}
