using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Team2 : MonoBehaviour , IPointerDownHandler{

    private bool IsSelected { get; set; }
    public int now;
    public Text _name;
    public Text _TextSelected;
    public Image PlayerImage, PlayerBack;
    private Toggle MyToggle = null;
    // Use this for initialization
    void Start()
    {
/*        PlayerImage.sprite = Resources.Load<Sprite>("AttackResources/" + Manager._selectedName[now]) as Sprite;
        PlayerBack.sprite = Resources.Load<Sprite>("AttackResources/" + Manager._selectedName[now]) as Sprite;
        _name.text = Manager._selectedName[now].ToString();
        */
        MyToggle = gameObject.GetComponent<UnityEngine.UI.Toggle>();
        IsSelected = false;
    }
    void Update()
    {
        if (Manager2._selectedName.Count >= Manager2._countLimit && _TextSelected.text == "未選取")
        {
            MyToggle.isOn = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Manager2 manager = GameObject.Find("Manager").GetComponent<Manager2>();
        IsSelected = manager.Select(_name.text);
        if (IsSelected)
            _TextSelected.text = "已選取";
        else
            _TextSelected.text = "未選取";
    }
}
