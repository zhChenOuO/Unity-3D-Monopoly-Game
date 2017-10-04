using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    static public int _countLimit = 4;
    static public List<string> _selectedName = new List<string>();
    public Text _selectedStatus;

	// Use this for initialization
	void Start () {
        _selectedStatus.text = "Not selected " + _selectedName.Count.ToString() + "/" + _countLimit.ToString();
	}
	
    public bool Select(string name) //選擇名稱
    {
        int selectIndex = Search(name);
        bool selectStatus = false;
        if (selectIndex != -1)
        {
            _selectedName.RemoveAt(selectIndex);
            selectStatus = false;
        }
        else
        {
            if (!IsFull())
            {
                _selectedName.Add(name);
                selectStatus = true;
            }
        }
        RefreshText();
        return selectStatus;
    }

    private void RefreshText() // 判斷有沒有選擇 和刪除不選的人
    {
        if (_selectedName.Count == 0)
            _selectedStatus.text = "Not selected " + _selectedName.Count.ToString() + "/" + _countLimit.ToString();
        else
        {
            string persons = "";
            foreach (var item in _selectedName)
                persons += item + ", ";
            _selectedStatus.text = persons + "chosen " + _selectedName.Count.ToString() + "/" + _countLimit.ToString();
        }
    }

    public bool IsFull() //判斷是否滿人
    {
        return _selectedName.Count == _countLimit;
    }

    private int Search(string name) //搜尋是否重複
    {
        for (int i = 0; i < _selectedName.Count; i++)
        {
            if (name == _selectedName[i])
                return i;
        }
        return -1;
    }
}
