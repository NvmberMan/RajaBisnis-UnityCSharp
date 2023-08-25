using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Rendering.Universal;

public class WorldLight : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    public Light2D _light;

    void Update()
    {
        float percentage = Mathf.Clamp01(GameManager.instance.time / 1444);
        _light.color = gradient.Evaluate(percentage);
    }
}
