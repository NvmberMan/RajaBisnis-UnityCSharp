using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public List<Dialog> lines = new List<Dialog>();
    public DialogPosition[] position;
    public DialogPosition defaultPosition;
    private int index;

    DialogPosition dp;
    public bool startOnAwake = false;
    // Start is called before the first frame update
    void Start()
    {
        checkPositino();

        dp.textComponent.text = string.Empty;

        if(startOnAwake)
            StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDialogue()
    {
        index = 0;
        if(gameObject.activeSelf)
            StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        checkPositino();
        dp.textComponent.text = string.Empty;
        GameManager.instance.spawnSoundEfx(GameManager.instance.clickClip, 1, 0.7f);

        foreach (DialogPosition pos in position)
        {
            pos.gameObject.SetActive(false);
        }

        lines[index].toDo.Invoke();
        dp.gameObject.SetActive(true);
        foreach (char c in lines[index].text.ToCharArray())
        {
            dp.textComponent.text += c;
            yield return new WaitForSeconds(lines[index].typeSpeed);
        }
    }

    void checkPositino()
    {
        dp = defaultPosition;

        if (lines[index].position)
            dp = lines[index].position;
    }
    public void NextLine(bool skip = false)
    {

        //check skip or next

        if(lines[index].position)
            dp = lines[index].position;



        if (dp.textComponent.text == lines[index].text || skip)
        {
            StopAllCoroutines();
            next();
        }
        else
        {
            StopAllCoroutines();
            dp.textComponent.text = lines[index].text;
        }



    }

    void next()
    {
        if (index < lines.Count - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class Dialog{
    [TextAreaAttribute]
    public string text;
    public float typeSpeed = 0.04f;
    public DialogPosition position;
    public bool hasClicked = false;


    [Space(10)]
    public UnityEvent toDo;
}
