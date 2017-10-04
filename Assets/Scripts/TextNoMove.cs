using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextNoMove : MonoBehaviour {
    public string DisplayText;
    public Text TextPrefeb;
    private float DestroyAfter = 1.5f;
    private float Timer;

    // Use this for initializations
    void Start()
    {
        Timer = DestroyAfter;
        TextPrefeb = GetComponentInChildren<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Destroy(gameObject);
        }
        if (DisplayText != null)
        {
            TextPrefeb.text = DisplayText;
        }
        TextPrefeb.text = DisplayText;
    }
}
