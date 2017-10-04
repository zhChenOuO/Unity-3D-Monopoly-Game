using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoryText : MonoBehaviour {
    public Text storyText;
    public Text contentText;
    public Text nextText;
    public string DisplayText1;
    public string DisplayText2;
    public GameObject self;
    public bool start = false;
    public bool start2 = false;
    public bool start3 = false;
    public bool start4 = false;
    public void Update()
    {
        gameObject.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        if (start == true)
        {
            StartCoroutine(AnimationText(DisplayText1));
        }
        if(start2 == true)
        {
            StartCoroutine(AnimationText(DisplayText2));
        }
        if(start3==true)
        {
            StartCoroutine(AnimationText("任意鍵繼續 ..."));
        }
        if(start4 ==true )
        {
            if(Input.anyKeyDown)
            {
                close();
            }
        }
    }
    IEnumerator AnimationText(string strComplete)
    {
        int i = 0;
        string str = "";
        if(start == true)
        {
            start = false;
            while (i < strComplete.Length)
            {
                str += strComplete[i++];
                storyText.text = str;
                yield return WaitForRealSeconds(0.07f);
            }
            start2 = true;
        }
        else if (start2 == true)
        {
            start2 = false;
            while (i < strComplete.Length)
            {
                str += strComplete[i++];
                contentText.text = str;
                yield return WaitForRealSeconds(0.07f);
            }
            start3 = true;
        }
        else if (start3 == true)
        {
            start3 = false;
            while (i < strComplete.Length)
            {
                str += strComplete[i++];
                nextText.text = str;
                yield return WaitForRealSeconds(0.09f);
            }
            start4 = true;
            while (true)
            {
                //set the Text's text to blank
                nextText.text = "";
                //display blank text for 0.5 seconds
                yield return WaitForRealSeconds(.5f);
                //display “I AM FLASHING TEXT” for the next 0.5 seconds
                nextText.text = str;
                yield return WaitForRealSeconds(.5f);
            }
        }
    }
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
    public void close()
    {
        Time.timeScale = 1;
        GameObject.Find("Canvas").GetComponent<Control>().next.enabled = true;
        Destroy(self);
    }
}
