using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private float slowTimeScale;
    [SerializeField] private float defaultTimeScale;
    private bool slowTimeToggle;

    private void Start()
    {
        slowTimeToggle = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            slowTimeToggle = !slowTimeToggle;
            Time.timeScale = slowTimeToggle ? slowTimeScale : defaultTimeScale;
        }
    }
}
