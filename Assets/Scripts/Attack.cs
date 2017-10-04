using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    static public string nameText = null;
    static public string story = null;
    private static GameObject Spikes;
    float time = 1.6f;
    void Update() //產生時間 
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            Destroy(Spikes);
            FloatText.story_text(story,nameText);
        }
    }
    public static void _Attack(GameObject Charactor) // 動態產生陷阱包含動作
    {
        Spikes = Instantiate(Resources.Load("Needle")) as GameObject;
        Spikes.transform.position = Charactor.transform.position;
        Spikes.transform.position -= new Vector3(0, 0.5f, 0);
        if (Control._held[Control.now_Player] == Control.Shield.NotHeld)
        {
            Control._playerHp[Control.now_Player] -= 50;
            FloatText.Click(Charactor, 50, "-");
            story = "這迷宮不知道是什麼，走著走著竟然還踩到陷阱 !";
            nameText = Manager._selectedName[Control.now_Player] + "  踩到陷阱，減少了50點生命";
        }
        else
        {
            story = "還好身上還放著保護的裝置，不過受到攻擊後好像被破壞掉了...";
            nameText = Manager._selectedName[Control.now_Player] + "  抵擋住攻擊 ，沒有受到任何攻擊";
            Control._held[Control.now_Player] = Control.Shield.NotHeld;
        }
    }
    public static void MonsterAttack(GameObject Charactor)     // 動態產生怪物包含動作
    {
        Control._playerHp[Control.now_Player] -= 200;
        Spikes = Instantiate(Resources.Load("Monster/Monster")) as GameObject;
        Spikes.transform.position = Charactor.transform.position;
        Spikes.transform.position += new Vector3(0,0,3f);

        if (Control._held[Control.now_Player] == Control.Shield.NotHeld)
        {
            story = "看到了鬼火獸，沒辦法躲避他的攻擊，受到了一點損傷";
            nameText = Manager._selectedName[Control.now_Player] + "  遭受怪物襲擊，減少了200點生命";
            FloatText.Click(Charactor, 200, "-");
            Time.timeScale = 0;
        }
        else
        {
            story = "還好身上還放著保護的裝置，不過受到攻擊後好像被破壞掉了...";
            nameText = Manager._selectedName[Control.now_Player] + "  抵擋住攻擊 ，沒有受到任何攻擊";
            Control._held[Control.now_Player] = Control.Shield.NotHeld;
        }
    }
}
