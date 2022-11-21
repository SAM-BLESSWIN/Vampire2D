using UnityEngine;
using UnityEngine.UI;

public class VampireEffect : MonoBehaviour
{
    [SerializeField] private float sunTimer;
    [SerializeField] private Image img_survivebar;
    [SerializeField] private PlayerController playerController;

    private float time = 0;

    private bool activateTimer;

    void Update()
    {
        if (!activateTimer) return;

        time += Time.deltaTime;
        if(time <= sunTimer)
        {
            img_survivebar.fillAmount = 1 - time / sunTimer;
        }
        else
        {
            playerController.Dead();
            ActivateEffect(false);
        }
    }

    public void ActivateEffect(bool value)
    {
        activateTimer = value;
        playerController.Hurt(value);
    }
}
