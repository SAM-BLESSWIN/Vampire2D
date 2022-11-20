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
            SwitchToDayView();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SwitchToNightView();
    }

    private void SwitchToNightView()
    {
        cam.LayerCullingHide(dayLayerMask);
        cam.LayerCullingShow(nightLayerMask);
        cam.backgroundColor = nightColor;
        canSwitchToDay = true; //test
    }

    private void SwitchToDayView()
    {
        cam.LayerCullingHide(nightLayerMask);
        cam.LayerCullingShow(dayLayerMask);
        cam.backgroundColor = dayColor;
        canSwitchToDay = false; //test
    }
}
