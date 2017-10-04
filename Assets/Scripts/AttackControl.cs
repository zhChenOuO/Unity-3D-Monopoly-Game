using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackControl : MonoBehaviour {
    // Use this for initialization
    public Button attack ;
    public Animator charactorAnim ;

    public Canvas MainCanvas;
    public Canvas AttackCanvas;
    public Text[] _name = new Text[Manager._countLimit - 1];
    public Image[] PlayerImage, PlayerBack = new Image[Manager._countLimit - 1];

    public bool Swtich = false;
    public float Timer ;
    static public int now = 0;
    void Update()
    {
        if (Swtich == true)
        {
            Timer -= Time.deltaTime;
            if(Timer<0.6f)
            {
                GameObject.Find(Manager._selectedName[Control.now_Player] + "Arrow").GetComponent<MeshRenderer>().enabled = false;
            }
            if (Timer < 0)
            {
                Control._playerHp[now] -= 100;
                GameObject.Find("Canvas").GetComponent<Control>().GetTargetCamera(GameObject.Find(Manager._selectedName[now]));
                FloatText.Click(GameObject.Find(Manager._selectedName[now]),100, "-");
                FloatText.story_text(Manager._selectedName[now]+"寫著:"+ Manager._selectedName[Control.now_Player] + "好像被鬼火獸的毒所侵害了，竟然在晚上的時候爬起來攻擊我。"
                    , Manager._selectedName[now]+"被"+ Manager._selectedName[Control.now_Player] + "襲擊損傷了 100點生命");
                Time.timeScale = 0;
                GameObject.Find("Canvas").GetComponent<Control>().dice.enabled = true;
                GameObject.Find("Canvas").GetComponent<Control>().next.enabled = false;
                Swtich = false;
            }
        }
        if(GridContent.attack == true)
        {

            if (Manager2._selectedName.Count == Manager2._countLimit)
            {
                attack.enabled = true;
            }
            else
            {
                attack.enabled = false;
            }
        }
    }
    void Start () { 
        attack = GameObject.Find("SelectButton").GetComponent<Button>();
        attack.enabled = false;
    }
    public void AttackGO() //攻擊
    {
        charactorAnim = GameObject.Find(Manager._selectedName[Control.now_Player]).GetComponent<Animator>();
        charactorAnim.SetBool("isBow", true);
        charactorAnim.SetBool("isIdle", false);
        charactorAnim.SetBool("isRunning", false);
        charactorAnim.SetBool("isSit", false);
        GameObject.Find(Manager._selectedName[Control.now_Player] + "Arrow").GetComponent<MeshRenderer>().enabled = true;
        for (int i = 0; i < Manager._countLimit; i++)
        {
            if (Manager._selectedName[i] == Manager2._selectedName[0])
            {
                now = i;
                FloatText.story_text("剛才鬼火獸劃傷了。有時候都會失去意識。在夜晚好像還會攻擊自己的同伴...", Manager._selectedName[Control.now_Player] + "  攻擊了  " + Manager._selectedName[now]);
                Time.timeScale = 0;
                break;
            }
        }
        if (Control._held[now] == Control.Shield.Withstand)
        {
            Swtich = false;
            Control._held[now] = Control.Shield.NotHeld;
            FloatText.story_text("還好身上還放著保護的裝置，不過受到攻擊後好像被破壞掉了...", Manager._selectedName[now] + "使用了道具抵擋住攻擊");
            Time.timeScale = 0;
        }
        else
        {
            Swtich = true;
        }
        MainCanvas.enabled = true;
        AttackCanvas.enabled = false;
        for (int i = 1; i <= Manager._countLimit - 1; i++)
            GameObject.Find("Player" + i).GetComponent<Toggle>().isOn = false;
        Manager2._selectedName = new List<string>();
        Swtich = true;
        Timer = 3;
    }
    static public void setImage(GameObject charactor) //印出可攻擊的對象
    {
        for(int i = 0,j=1; i<Manager._countLimit;i++)
        {
            if (charactor.name != Manager._selectedName[i])
            {
                if (Control._state[i] != Control.PlayerState.Death)
                {
                    GameObject.Find("Play" + j + "BackGround").GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackResources/" + Manager._selectedName[i]) as Sprite;
                    GameObject.Find("Player" + j).GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackResources/" + Manager._selectedName[i]) as Sprite;
                    GameObject.Find("Name" + j).GetComponent<Text>().text = Manager._selectedName[i];
                }
                else
                {
                    GameObject.Find("Play" + j + "BackGround").GetComponent<Image>().enabled = false;
                    GameObject.Find("Player" + j).GetComponent<Image>().enabled = false;
                    GameObject.Find("Name" + j).GetComponent<Text>().enabled = false;
                    GameObject.Find("Player" + j).GetComponent<Toggle>().isOn = false;
                }
                j++;
            }
        }
    }
    static public int getNow()
    {
        return now;
    }
}
