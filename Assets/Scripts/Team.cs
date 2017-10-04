using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Team : MonoBehaviour, IPointerDownHandler {

    private bool IsSelected { get; set; }
    public Text _name;
    public Text _TextSelected;
    private Toggle MyToggle = null;
    // Use this for initialization
    void Start () {
        MyToggle = gameObject.GetComponent<UnityEngine.UI.Toggle>();
        IsSelected = false;
	}
	void Update()
    {
        if(Manager._selectedName.Count>=Manager._countLimit && _TextSelected.text == "未選取")
        {
            MyToggle.isOn = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
        IsSelected = manager.Select(_name.text);
        if (IsSelected)
            _TextSelected.text = "已選取";
        else
            _TextSelected.text = "未選取";
    }
}
