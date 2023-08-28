using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Rendering.Universal;

public class WorldLight : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    public Light2D _light;

    private void Start()
    {
        StartCoroutine(updateWorldLight());
    }
    IEnumerator updateWorldLight()
    {
        yield return new WaitForSeconds(1);
        updateLight();
    }

    void updateLight()
    {
        float percentage = Mathf.Clamp01(GameManager.instance.time / 1444);
        _light.color = gradient.Evaluate(percentage);
    }
}
