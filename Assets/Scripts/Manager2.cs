using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class Manager2 : MonoBehaviour { 

    static public int _countLimit = 1;
    static public List<string> _selectedName = new List<string>();
    public Text _selectedStatus;

    // Use this for initialization
    void Start()
    {
        _selectedStatus.text = "Not selected " + _selectedName.Count.ToString() + "/" + _countLimit.ToString();
    }
    public bool Select(string name) //攻擊畫面的選擇角色
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

    private void RefreshText()
    {
        if (_selectedName.Count == 0)
            _selectedStatus.text = "NOT CHOOSE " + _selectedName.Count.ToString() + "/" + _countLimit.ToString();
        else
        {
            _selectedStatus.text = "CHOOSE :" +_selectedName[0]+ " , "+_selectedName.Count.ToString() + "/" + _countLimit.ToString();
        }
    }

    public bool IsFull()
    {
        return _selectedName.Count == _countLimit;
    }

    private int Search(string name)
    {
        for (int i = 0; i < _selectedName.Count; i++)
        {
            if (name == _selectedName[i])
                return i;
        }
        return -1;
    }
}
