using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PromptText : MonoBehaviour {

    public string DisplayText;
    public Text TextPrefeb;
    private float Speed;
    private float DestroyAfter;
    private float Timer;

    // Use this for initialization
    void Start()
    {
        Speed = 3200.0f;
        DestroyAfter = 3f;
        Timer = DestroyAfter;
        TextPrefeb = GetComponentInChildren<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if(transform.position.x >482 && transform.position.x<542)
        {
            gameObject.transform.position = new Vector2(512,644);
            Timer -= Time.deltaTime;
            if(Timer<.2f)
                transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.World);
        }
        else
        {
            if (Timer < 0)
            {
                Destroy(gameObject);
                if(Control._state[Control.now_Player] == Control.PlayerState.Death)
                {
                    GameObject.Find("Canvas").GetComponent<Control>().dice.enabled = false;
                    GameObject.Find("Canvas").GetComponent<Control>().next.enabled = true;
                }
                else
                {
                    GameObject.Find("Canvas").GetComponent<Control>().dice.enabled = true;
                    GameObject.Find("Canvas").GetComponent<Control>().next.enabled = false;
                }
            }
            if (DisplayText != null)
            {
                TextPrefeb.text = DisplayText;
            }
            TextPrefeb.text = DisplayText;
            if (Speed > 0)
            {
                transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.World);
            }
        }
    }
}
