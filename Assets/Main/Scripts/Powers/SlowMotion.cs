using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private float powerTime;
    [SerializeField] private float reloadTime;
    [SerializeField] private float slowTimeScale;
    [SerializeField] private float defaultTimeScale;

    [Header("UI")]
    [SerializeField] private Image img_radialBar;

    private bool canSlowTime;
    private bool canReloadTime;
    private bool isPowerActivated;
    private float elapsedTime = 0;

    private void Start()
    {
        canSlowTime = true;
        canReloadTime = false;
        isPowerActivated = false;
    }

    void Update()
    {
        if(canReloadTime)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime <= reloadTime)
            {
                img_radialBar.fillAmount = elapsedTime / reloadTime;
            }
            else
            {
                elapsedTime = 0;
                canReloadTime = false;
                canSlowTime=true;
            }
        }
        
        if(isPowerActivated)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (elapsedTime <= powerTime)
            {
                img_radialBar.fillAmount = 1 - elapsedTime / powerTime;
            }
            else
            {
                elapsedTime = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.Q) && canSlowTime)
        {
            StartCoroutine(StartSlowMotionEffect());
        }
    }

    IEnumerator StartSlowMotionEffect()
    {
        Time.timeScale = slowTimeScale;
        isPowerActivated=true;
        canSlowTime = false;
        yield return new WaitForSecondsRealtime(powerTime);

        isPowerActivated = false;
        canReloadTime=true;
        Time.timeScale = defaultTimeScale;
    }

    public void Reset()
    {
        Time.timeScale=defaultTimeScale;
        elapsedTime=0;
        img_radialBar.fillAmount=0;
        isPowerActivated = false;
        canSlowTime = false;
        canReloadTime = true;
    }
}
