using System.Collections;
using System.Threading;
using UnityEngine;
using TMPro;
public class FPSCounter : MonoBehaviour
{
    [Header("Frame Settings")]
    int MaxRate = 9999;
    public float TargetFrameRate = 60.0f;
    float currentFrameTime;
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = MaxRate;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine("WaitForNextFrame");
        StartCoroutine(nextFrame());
    }
    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentFrameTime += 1.0f / TargetFrameRate;
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameTime - t - 0.01f;
            if (sleepTime > 0)
                Thread.Sleep((int)(sleepTime * 1000));
            while (t < currentFrameTime)
                t = Time.realtimeSinceStartup;
        }
    }

    IEnumerator nextFrame()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<TMP_Text>().text = "FPS : " + (1f / Time.deltaTime).ToString();

        StartCoroutine(nextFrame());
    }
}