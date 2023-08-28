using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlertUI : MonoBehaviour
{
    [HideInInspector]public string text;
    [HideInInspector]public float destroyDelayed;
    [SerializeField] private TMP_Text textText;
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        textText.text = text;
        Invoke("destroyThis", destroyDelayed);
    }

    void destroyThis()
    {
        anim.Play("DestroyAlert");
        Destroy(this.gameObject, 1f);
    }
}
