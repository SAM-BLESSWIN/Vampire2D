using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] private LevelCompletionController levelCompletionController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            if(playerController.IsDead) return;

            SoundManager.Instance.Play(SoundTypes.LEVELCOMPLETED);
            LevelManager.Instance.MarkCurrentLevelComplete();
            levelCompletionController.LevelCompleted();
        }
    }
}
