using UnityEngine;
using UnityEngine.UI;
public class JasperControls : MonoBehaviour {
	Animator anim;
	public float speed = 4f;
    public GameObject Obj;
	private GameObject endPoint;
    public GameObject quad;
    private int i = 0;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
        bool count = false;
        for(int i =0;i<Manager._countLimit;i++)
        {
            if(Obj.name == Manager._selectedName[i])
            {
                count = true;
                Obj.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
        if (count == false)
        {
            Destroy(GameObject.Find(Obj.name + "Quad"));
            Destroy(Obj);
        }
            
	}
	
	// Update is called once per frame
	void Update () {
        if (Obj.name == Manager._selectedName[Control.now_Player] && Control.now_step > 0 && Control.GetState == Control.PlayerState.Start) {
            anim.SetBool("isSit", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isBow", false);
            anim.SetBool ("isRunning", true);
            endPoint = GameObject.Find ("Block" + Control._playerPossition[Control.now_Player]%40);

			transform.position = Vector3.MoveTowards (transform.position, endPoint.transform.position, Time.deltaTime * 8);
            quad.transform.position = transform.position;
            quad.transform.position += new Vector3(0, 50, 0);
			float tmp = Vector3.Distance (Obj.transform.position, endPoint.transform.position);
			if (tmp == 0) {
				i++;
                Control._playerPossition[Control.now_Player]++;
                if (i > 1) {
                    Control.now_step--;
				}//左↓
                if (Control._playerPossition[Control.now_Player] % 40 == 1 ||
                    Control._playerPossition[Control.now_Player] % 40 == 21|| 
                    Control._playerPossition[Control.now_Player] % 40 == 23||
                    Control._playerPossition[Control.now_Player] % 40 == 29) {
                    Obj.transform.rotation = Quaternion.Euler (0, -90, 0);
                }//上↓
                if (Control._playerPossition[Control.now_Player] % 40 == 4|| 
                    Control._playerPossition[Control.now_Player] % 40 == 14|| 
                    Control._playerPossition[Control.now_Player] % 40 == 22|| 
                    Control._playerPossition[Control.now_Player] % 40 == 35||
                    Control._playerPossition[Control.now_Player] % 40 == 39) {
                    Obj.transform.rotation = Quaternion.Euler(0, 0, 0);
                }//右↓
                if (Control._playerPossition[Control.now_Player] % 40 == 7||
                    Control._playerPossition[Control.now_Player] % 40 == 12||
                    Control._playerPossition[Control.now_Player] % 40 == 16|| 
                    Control._playerPossition[Control.now_Player] % 40 == 26||
                    Control._playerPossition[Control.now_Player] % 40 == 36) {
                    Obj.transform.rotation = Quaternion.Euler(0, 90, 0);
                }//下↓
                if (Control._playerPossition[Control.now_Player] % 40 == 10 ||
                    Control._playerPossition[Control.now_Player] % 40 == 17 ||
                    Control._playerPossition[Control.now_Player] % 40 == 24 ||
                    Control._playerPossition[Control.now_Player] % 40 == 28)
                {
                    Obj.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            if (Control.now_step == 0)
            {
                i = 0;
                int count1 = 0;
                Control._playerPossition[Control.now_Player]--;
                for (int j = 0; j < Manager._countLimit; j++) 
                {
                    if (Control._playerPossition[j] == Control._playerPossition[Control.now_Player]) 
                    {
                        count1++;
                    }
                }
                if (count1 == 1) Obj.transform.position += new Vector3(-0.6f, 0, -0.6f);
                else if (count1 == 2) Obj.transform.position += new Vector3(-0.6f, 0, 0.6f);
                else if (count1 == 3) Obj.transform.position += new Vector3(0.6f, 0, -0.6f);
                else if (count1 == 4) Obj.transform.position += new Vector3(0.6f, 0, 0.6f);
                Control._playerHp[Control.now_Player] -= 5;
                //射箭(獲得武器)
                //監牢
                if (Control._playerPossition[Control.now_Player] % 40 == 19)
                {
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isBow", false);
                    anim.SetBool("isSit", true);

                }
                //死亡
                else if (Control._playerPossition[Control.now_Player] % 40 == 9)
                {
                    anim.SetBool("isSit", false);
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isBow", false);
                    anim.SetBool("isRunning", false);
                }
                //站立
                else
                {
                    anim.SetBool("isSit", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isBow", false);
                    anim.SetBool("isIdle", true);
                }
                FloatText.Click(Obj,5,"-");
            }
        }
	}
}
