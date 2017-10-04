using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridContent : MonoBehaviour {
    public bool isDone;
    public GameObject Jail;
    public Button dice;
    public Button next;
    static public bool attack = false;
    public Canvas MainCanvas = new Canvas();
    public Canvas AttackCanvas = new Canvas();
    private void Update()
    {
        for (int i = 0; i < Manager._countLimit; i++)
        {
            if (Control._playerPossition[i] == 19)
                Jail.SetActive(true);
        }
        GameObject charactor = GameObject.Find(Manager._selectedName[Control.now_Player]);
        if (charactor.name == Manager._selectedName[Control.now_Player] &&
            Control._state[Control.now_Player] == Control.PlayerState.Start&&
            Control.now_step == 0 )
        {
            Animator charactorAnim = charactor.GetComponent<Animator>();
            Control._state[Control.now_Player] = Control.PlayerState.Stop;
            Grid(charactor,charactorAnim);
        }
    }
    public void Grid(GameObject Charactor,Animator anim) //執行格子內容
    {
        if (Control._playerPossition[Control.now_Player] == 0)
        {
            Control._playerHp[Control.now_Player] += 300;
            FloatText.Click(Charactor, 300,"+");
            FloatText.story_text("來到這個迷宮不知道多久了,還沒找到全部的裝置...",
                Manager._selectedName[Control.now_Player] + "  Get  props ");
            Time.timeScale = 0;
        }
        //過關道具
        else if (Control._playerPossition[Control.now_Player] %40==1  ||//1
            Control._playerPossition[Control.now_Player] % 40 == 10 ||
            Control._playerPossition[Control.now_Player] % 40 == 20 ||
            Control._playerPossition[Control.now_Player] % 40 == 23 ||
            Control._playerPossition[Control.now_Player] % 40 == 28 ||
            Control._playerPossition[Control.now_Player] % 40 == 32)
        {
            int number = -1;
            if (Control._playerPossition[Control.now_Player] % 40 == 1) number = 0;
            else if (Control._playerPossition[Control.now_Player] % 40 == 10) number = 1;
            else if (Control._playerPossition[Control.now_Player] % 40 == 20) number = 2;
            else if (Control._playerPossition[Control.now_Player] % 40 == 23) number = 3;
            else if (Control._playerPossition[Control.now_Player] % 40 == 28) number = 4;
            else number = 5;
            if(Control._props[number] ==true)
            {
                FloatText.story_text("發現了鬼火獸的裝置，看著上面的編號好像已經有人拿過了...",
                Manager._selectedName[Control.now_Player] + " 獲得重複編號為" + (number + 1) + "的鬼火獸裝置");
            }
            else
            {
                Control._props[number] = true;
                FloatText.story_text("發現了鬼火獸的裝置，該趕快回去跟他們會合，和他們說這件事情...",
               Manager._selectedName[Control.now_Player] + " 得到了上面有著編號" + (number + 1) + "的鬼火獸裝置");
            }
            GameObject.Find("Canvas").GetComponent<Control>().GetProps();
        }
        //獲得武器V
        else if (Control._playerPossition[Control.now_Player] % 40 == 3 ||
            Control._playerPossition[Control.now_Player] % 40 == 7 ||
            Control._playerPossition[Control.now_Player] % 40 == 13 ||
            Control._playerPossition[Control.now_Player] % 40 == 17 ||
            Control._playerPossition[Control.now_Player] % 40 == 22 ||
            Control._playerPossition[Control.now_Player] % 40 == 30 ||
            Control._playerPossition[Control.now_Player] % 40 == 35 ||
            Control._playerPossition[Control.now_Player] % 40 == 39)
        {
            AttackControl.setImage(Charactor);
            AttackCanvas.enabled = true;
            MainCanvas.enabled = false;
            attack = true;
        }
        //怪物
        else if (Control._playerPossition[Control.now_Player] % 40 == 5 ||//5
            Control._playerPossition[Control.now_Player] % 40 == 14 ||
            Control._playerPossition[Control.now_Player] % 40 == 25 ||
            Control._playerPossition[Control.now_Player] % 40 == 36)
        {
            Attack.MonsterAttack(GameObject.Find(Manager._selectedName[Control.now_Player]));
        }
        //抵擋V
        else if (Control._playerPossition[Control.now_Player] % 40 == 4 ||
            Control._playerPossition[Control.now_Player] % 40 == 15 ||
            Control._playerPossition[Control.now_Player] % 40 == 27 ||
            Control._playerPossition[Control.now_Player] % 40 == 37)
        {
            FloatText.story_text("不知道這裝置能幹什麼用。好像能保護自己的樣子...", 
                Manager._selectedName[Control.now_Player] + "  獲得防禦一次的機會");
            Control._held[Control.now_Player] = Control.Shield.Withstand;
        }
        //陷阱V
        else if (Control._playerPossition[Control.now_Player] % 40 ==8||
            Control._playerPossition[Control.now_Player] % 40 == 11 ||
            Control._playerPossition[Control.now_Player] % 40 == 18 ||
            Control._playerPossition[Control.now_Player] % 40 == 33)
        {
            Attack._Attack(GameObject.Find("Block" + Control._playerPossition[Control.now_Player] % 40));
        }
        //死亡V
        else if (Control._playerPossition[Control.now_Player] % 40 == 9)
        {
            anim.SetBool("isDeath", true);
            GameObject.Find("Canvas").GetComponent<Control>().next.enabled = true;
            Control._playerHp[Control.now_Player] = 0;
            Control._state[Control.now_Player] = Control.PlayerState.Death;
        }
        //監牢V
        else if (Control._playerPossition[Control.now_Player] % 40 == 19)
        {
            FloatText.story_text("被同伴發現身上有鬼火獸的傷口。被關在了監牢裡面...", 
                Manager._selectedName[Control.now_Player] + "  暫停一回合 ");
            Control._state[Control.now_Player] = Control.PlayerState.Sleep;
        }
        //隨機移動
        else if (Control._playerPossition[Control.now_Player] % 40 == 30)
        {
            FloatText.story_text("迷宮突然變換了。走著走著不知道到了那裡...",
                Manager._selectedName[Control.now_Player] + "  隨機移動 ");
            Control._playerPossition[Control.now_Player] += Random.Range(1, 39);
            Charactor.transform.position = GameObject.Find("Block" + Control._playerPossition[Control.now_Player] % 40).transform.position;
            Grid(Charactor,anim);
        }
        //獲得食物v
        else
        {
            Control._playerHp[Control.now_Player] += 200;
            FloatText.Click(Charactor, 200,"+");
            FloatText.story_text("還好有找到食物，不然已經沒東西吃了...",
                Manager._selectedName[Control.now_Player] + " 取得了食物 ，增加了200點生命");
            Time.timeScale = 0;
        }
        int count = 0;
        string name = null;
        for (int i = 0; i < 6; i++) //計算過關道具是否全部都有
        {
            if (Control._props[i] == true)
                count++;
            if(count ==6)
            {
                FloatText.WinText(Manager._selectedName[Control.now_Player]);
                Destroy(GameObject.Find("LatticeControl").GetComponent<GridContent>());
            }
        }//計算過關道具是否全部都有↑↑↑
        count = 0;
        for (int i = 0;i<Manager._countLimit;i++) //計算是否剩一位玩家
        {
            if (Control._state[i] != Control.PlayerState.Death)
            {
                count++;
                name = Manager._selectedName[i];
            }
        }
        if (count == 1)
        {
            FloatText.WinText(name);
            Destroy(GameObject.Find("LatticeControl").GetComponent<GridContent>());
        }
        else
        {
            if (Control._playerHp[Control.now_Player] <= 0)
            {
                GameObject.Find("GameOver").GetComponent<Image>().enabled = true;
            }
        }
        //計算是否剩一位玩家↑↑↑
    }
}
