using System.Collections;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    Camera cam;

    [Header("LayerMasks")]
    [SerializeField] private LayerMask dayLayerMask;
    [SerializeField] private LayerMask nightLayerMask;

    [Header("Color")]
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        SwitchToNightView();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<VampireEffect>(out VampireEffect vampireEffect))
        {
            SwitchToDayView();
            vampireEffect.ActivateEffect(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<VampireEffect>(out VampireEffect vampireEffect))
        {
            SwitchToNightView();
            vampireEffect.ActivateEffect(false);
        }
    }

    private void SwitchToNightView()
    {
        cam.LayerCullingHide(dayLayerMask);
        cam.LayerCullingShow(nightLayerMask);
        cam.backgroundColor = nightColor;
    }

    public void SwitchToDayView()
    {
        cam.LayerCullingHide(nightLayerMask);
        cam.LayerCullingShow(dayLayerMask);
        cam.backgroundColor = dayColor;
    }

    public void SwitchToDayView(float timer)
    {
        SwitchToDayView();
        StartCoroutine(TurnOff(timer));
    }

    IEnumerator TurnOff(float timer)
    {
        yield return new WaitForSeconds(timer);
        SwitchToNightView();
    }

}
