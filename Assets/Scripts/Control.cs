using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class Control : MonoBehaviour {
	// Use this for initialization
	static public bool clickOn = false;
	static public int now_Player = 0;
	static public int now_step = 0;

    static public int[] _playerPossition = new int[Manager._countLimit];
    static public int[] _playerHp = new int[Manager._countLimit];
    static public bool[] _props = new bool[6];

    public GameObject TestCanvas;

    private Camera[] cam = new Camera[Manager._countLimit + 1];
    public Camera allmapCamera = new Camera();
    public Camera DiceCamera = new Camera();
    public Canvas canvas = new Canvas();
    public Canvas attackCanvas = new Canvas();
    public Image DiceImage;
    public Image rules;

    static public PlayerState[] _state;
    static public Shield[] _held;

    public Button next, dice;
    public Image[] _propsImage = new Image[6];
    public Text testtext;

    void Start() //初始化
    {
        TestCanvas.SetActive(false);
        GameObject.Find("GameOver").GetComponent<Image>().enabled = false;
        _state = new PlayerState[Manager._countLimit];
        _held = new Shield[Manager._countLimit];
        for (int i = 0; i < Manager._countLimit; i++)
        {
            _held[i] = Shield.NotHeld;
            _state[i] = PlayerState.Stop;
            _playerPossition[i] = 0;
            _playerHp[i] = 1500;
            _props[i] = false;
            cam[i] = GameObject.Find(Manager._selectedName[i] + "Camera").GetComponent<Camera>();
            cam[i].enabled = false;
            if (cam[i].name == Manager._selectedName[now_Player] + "Camera")
            {
                cam[i].enabled = true;
            }
            if (i== 0) GameObject.Find(Manager._selectedName[i]).transform.position += new Vector3(-0.4f, 0, -0.4f);
            else if (i == 1) GameObject.Find(Manager._selectedName[i]).transform.position += new Vector3(-0.4f, 0, 0.4f);
            else if (i == 2) GameObject.Find(Manager._selectedName[i]).transform.position += new Vector3(0.4f, 0, -0.4f);
            else if (i == 3) GameObject.Find(Manager._selectedName[i]).transform.position += new Vector3(0.4f, 0, 0.4f);
        }
        next.enabled = false;
        DiceCamera.enabled = false;
        canvas.enabled = true;
        attackCanvas.enabled = false;
        rules.enabled = false;
    }
    private void Update() //偵測小是否切換小地圖
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            allmapCamera.enabled = true;
            canvas.enabled = false;
            for (int i = 0; i < Manager._countLimit; i++)
            {
                cam[i].enabled = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            DiceCamera.enabled = false;
            canvas.enabled = true;
            allmapCamera.enabled = false;
            GetCamera();
        }
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            TestCanvas.SetActive(!TestCanvas.activeSelf);
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            rules.enabled = !rules.enabled;
        }
        else
        {
            if (Dicemove.rb.IsSleeping() && DisplayCurrentValue.move == true)
            {
                DisplayCurrentValue.move = false;
                DiceCamera.enabled = false;
                GetCamera();
                DiceImage.sprite = Resources.Load<Sprite>("Dice/" + DisplayCurrentValue.currentValue + "Dice") as Sprite;
                Debug.Log(DisplayCurrentValue.currentValue);
            }
        }
    }
    static public void EndGame() //結束遊戲
    {
        Application.Quit();
    }
    public void TestText()
    {
        now_step = Convert.ToInt32(testtext.text);
        DiceCamera.enabled = false;
        GetCamera();
        dice.enabled = false;
        _state[now_Player] = PlayerState.Start;
    }
    public void Button_Click() //骰骰子按鈕
    {
        clickOn = true;
        DiceCamera.enabled = true;
        dice.enabled = false;
        next.enabled = false;
    }
    public void Next_Click() //下一個人
    {
        if (now_Player == Manager._countLimit - 1)  now_Player = 0;
        else now_Player++;

        if (_state[now_Player] == PlayerState.Death)
        {
            GetCamera();
            DiceImage.sprite = Resources.Load<Sprite>("Dice/Death") as Sprite;
        }
        else if (_state[now_Player] == PlayerState.Sleep)
        {
            _state[now_Player] = PlayerState.Stop;
            DiceImage.sprite = Resources.Load<Sprite>("Dice/Sleep") as Sprite;
        }
        else
        {
            GetProps();
            GetCamera();
            DiceImage.sprite = Resources.Load<Sprite>("Dice/DiceQ") as Sprite;
        }
        next.enabled = false;
        GameObject.Find("GameOver").GetComponent<Image>().enabled = false;
        FloatText.Prompt_text(Manager._selectedName[Control.now_Player] + "   Round");
    }
    public void GetProps() //獲得道具改成白色
    {
        for (int i = 0; i < 6; i++)
        {
            if (_props[i] == true)
            {
                _propsImage[i].color = new Color(255, 255, 255, 255);
            }
            else
            {
                _propsImage[i].color = new Color(0, 0, 0, 255);
            }
        }
    }
    public void GetCamera() //取得現在的玩家攝影機
    {
        for (int i = 0; i < Manager._countLimit; i++)
        {
            cam[i].enabled = false;
            if (cam[i].name == Manager._selectedName[now_Player] + "Camera")
            {
                cam[i].enabled = true;
            }
        }
    }
    public void GetTargetCamera(GameObject charactor) //取得被攻擊目標的攝影機
    {
        for (int i = 0; i < Manager._countLimit; i++)
        {
            cam[i].enabled = false;
            if (cam[i].name == charactor.name + "Camera")
            {
                cam[i].enabled = true;
            }
        }
    }
    public enum PlayerState
    {
        Start, Stop, Sleep, Death
        //開始,停止,暫停,死亡
    }
    public enum Shield
    {
        NotHeld, Withstand
        //沒有抵擋,有抵擋
    }
    static public PlayerState GetState
    {
        get { return _state[now_Player]; }
        set { _state[now_Player] = value; }
    }
}