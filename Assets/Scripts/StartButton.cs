using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StartButton : MonoBehaviour {
    private Button startbutton = null;
    void Start()
    {
        startbutton = gameObject.GetComponent<UnityEngine.UI.Button>();
    }
	void Update () {
        if (Manager._countLimit == Manager._selectedName.Count)
        {
            startbutton.enabled = true;
        }
        else
        {
            startbutton.enabled = false;
        }
	}
}
