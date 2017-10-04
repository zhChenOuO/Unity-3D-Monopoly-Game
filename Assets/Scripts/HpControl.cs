using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpControl : MonoBehaviour {
    public Text nameText;
    public Text nowPlaceText;
    public Text hpText;
    public Image hpImage;
    public Image Mask;
    public Image Shild;
    public float MaxHp = 1500;
    public int now;
    // Update is called once per frame
    public void Update()
    {
        float hp = Control._playerHp[now];
        if (hp > 1500)
            hp = 1500;
        if (hp < 0)
            hp = 0;
        hpImage.transform.localPosition = new Vector3(-223 + 223 * (hp / MaxHp), 0.0f, 0.0f);
        nameText.text = Manager._selectedName[now];
        if (nameText.text == Manager._selectedName[Control.now_Player])
            Mask.sprite = Resources.Load<Sprite>("Bar/Mask2") as Sprite;
        else
            Mask.sprite = Resources.Load<Sprite>("Bar/Mask1") as Sprite;
        nowPlaceText.text = "Now Place : " + (Control._playerPossition[now] % 40).ToString();
        hpText.text = Control._playerHp[now].ToString();
        if (Control._held[now] == Control.Shield.Withstand)
            Shild.enabled = true;
        else
            Shild.enabled = false;
    }
}
