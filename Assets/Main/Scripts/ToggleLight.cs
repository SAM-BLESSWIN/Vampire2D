using System.Collections;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    Camera cam;
    bool canSwitchToDay;

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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) //test
        {
            if(canSwitchToDay)
            {
                SwitchToDayView();
            }
            else
            {
                SwitchToNightView();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SwitchToDayView();
            collision.GetComponent<VampireEffect>().ActivateEffect(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SwitchToNightView();
            collision.GetComponent<VampireEffect>().ActivateEffect(false);
        }
    }

    private void SwitchToNightView()
    {
        cam.LayerCullingHide(dayLayerMask);
        cam.LayerCullingShow(nightLayerMask);
        cam.backgroundColor = nightColor;
        canSwitchToDay = true; //test
    }

    public void SwitchToDayView()
    {
        cam.LayerCullingHide(nightLayerMask);
        cam.LayerCullingShow(dayLayerMask);
        cam.backgroundColor = dayColor;
        canSwitchToDay = false; //test
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
