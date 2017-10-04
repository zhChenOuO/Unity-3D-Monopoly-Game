using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextOnSport : MonoBehaviour {

    public string DisplayText;
    public Text TextPrefeb;
    private float Speed;
    private float DestroyAfter ;
    private float Timer;

    // Use this for initialization
    void Start()
    {
        Speed = 100.0f;
        DestroyAfter = 1.5f;
        Timer = DestroyAfter;
        TextPrefeb = GetComponentInChildren<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            GameObject.Find("Canvas").GetComponent<Control>().GetCamera();
            Destroy(gameObject);
        }
        if (DisplayText != null)
        {
            TextPrefeb.text = DisplayText;
        }
        TextPrefeb.text = DisplayText;
        if (Speed > 0)
        {
            transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.World);
        }
    }
}
