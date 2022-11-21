using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeReference]private Switch activateSwitch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            SoundManager.Instance.Play(SoundTypes.COLLECTABLES);
            activateSwitch.Activate();
            Destroy(this.gameObject);
        }
    }
}
