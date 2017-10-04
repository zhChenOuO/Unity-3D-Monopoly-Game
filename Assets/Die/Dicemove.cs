using UnityEngine;
using System.Collections;

public class Dicemove : MonoBehaviour {
	public ForceMode forceMode ;
	static public Rigidbody rb;
	static public bool move=false;
    public float forceAmount ;
    public float Torque ;
    static public bool ok = false;
	// Use this for initializatio
	void Start(){
		rb = GetComponent<Rigidbody>();
	}
	// Update is called once per frame
	void Update () {
		if (Control.clickOn == true) {
            Control.clickOn = false;
            float forceAmount = Random.Range(3.0f, 5.0f);
            float Torque = Random.Range(20.0f, 25.0f);
            rb.AddForce (Random.onUnitSphere * forceAmount , forceMode);
			rb.AddTorque (Random.onUnitSphere * Torque, forceMode);
            ok = true;
		}
		if (rb.IsSleeping () && ok == true) {
			ok = false;
			move = true;
            Control._state[Control.now_Player] = Control.PlayerState.Start;
        }
	}
}
