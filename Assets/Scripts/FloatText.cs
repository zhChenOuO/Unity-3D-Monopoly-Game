using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatText : MonoBehaviour {

    static private string TextToShow;
    static private GameObject canvas;
    static private TextOnSport givePointsText;
    static private TextNoMove givePointsText2;
    static public void Click(GameObject Charactor, int hp,string sign)
    {
        if(sign =="+")
        {
            PrintText(Charactor, hp, sign, "text1");
            PrintText(Charactor, hp, sign, "text3");
        }
        else
        {
            PrintText(Charactor, hp, sign, "text");
            PrintText(Charactor, hp, sign, "text2");
        }
    }
    static public void PrintText(GameObject Charactor , int hp , string sign ,string text) // 印出被攻擊或獲得生命值 
    {
        int now = 0;
        for (int i = 0; i < Manager._countLimit; i++)
        {
            if (Charactor.name == Manager._selectedName[i])
                now = i;
        }
        now++;
        canvas = GameObject.Find("Canvas");
        GameObject PointText = Instantiate(Resources.Load(text)) as GameObject;
        if(PointText.GetComponent<TextOnSport>() != null)
        {
            TextOnSport GivePointsText;
            GivePointsText = PointText.GetComponent<TextOnSport>();
            TextToShow = sign + hp.ToString();
            GivePointsText.DisplayText = TextToShow;
        }
        else if(PointText.GetComponent<TextNoMove>() != null)
        {
            TextNoMove GivePointsText;
            GivePointsText = PointText.GetComponent<TextNoMove>();
            TextToShow = sign + hp.ToString();
            GivePointsText.DisplayText = TextToShow;
        }
        if(text == "text" || text == "text1")
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(Charactor.transform.position);
            screenPosition += new Vector2(30, 400);
            PointText.transform.SetParent(canvas.transform, false);
            PointText.transform.position = screenPosition;
            TextToShow = null;
        }
        else if(text == "text2" || text == "text3")
        {
            Vector2 screenPosition = GameObject.Find("Character" + now).transform.position;
            screenPosition += new Vector2(-50, 55);
            PointText.transform.SetParent(canvas.transform, false);
            PointText.transform.position = screenPosition;
        }
    }
    static public void WinText(string name) //贏得勝利文字
    {
        canvas = GameObject.Find("Canvas");
        GameObject PointText = Instantiate(Resources.Load("Winner")) as GameObject;
        PointText.GetComponentInChildren<Button>().onClick.AddListener(Control.EndGame);
        string Allname = null;
        for (int i = 0; i < Manager._countLimit;i++ )
        {
            if(Control._state[i]!= Control.PlayerState.Death)
            {
                Allname += Manager._selectedName[i] + " ";
            }
        }
        PointText.transform.Find("WinText").GetComponent<Text>().text = Allname + "\r\nEscape the maze";

        PointText.transform.SetParent(canvas.transform, false);
        Destroy(GameObject.Find("Run"));
    }
    static public void Prompt_text(string name) //名字文字
    {
        string TextToShow;
        GameObject canvas;
        canvas = GameObject.Find("Canvas");
        GameObject PointText = Instantiate(Resources.Load("NameText")) as GameObject;
        if (PointText.GetComponent<PromptText>() != null)
        {
            PromptText GivePointsText;
            GivePointsText = PointText.GetComponent<PromptText>();
            TextToShow = name;
            GivePointsText.DisplayText = TextToShow;
        }
        PointText.transform.position += new Vector3(-512, 0, 0);
        PointText.transform.SetParent(canvas.transform, false);
        TextToShow = null;
    }
    static public void story_text(string story,string name) //內容文字
    {
        GameObject canvas;
        canvas = GameObject.Find("Canvas");
        GameObject PointText = Instantiate(Resources.Load("StoryUI")) as GameObject;
        if (PointText.GetComponent<StoryText>() != null)
        {
            StoryText GivePointsText;
            GivePointsText = PointText.GetComponent<StoryText>();
            GivePointsText.DisplayText1 = story;
            GivePointsText.DisplayText2 = name;
            GivePointsText.start = true;
        }
        PointText.transform.SetParent(canvas.transform, false);
        TextToShow = null;
    }
}
