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
        StartCoroutine(updateTime());
    }

    void Update()
    {

    }

    IEnumerator updateTime()
    {
        float percentage = Mathf.Clamp01(GameManager.instance.time / 1444);
        _light.color = gradient.Evaluate(percentage);

        yield return new WaitForSeconds(2);

        StartCoroutine(updateTime());

    }
}
