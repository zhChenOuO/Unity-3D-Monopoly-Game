using UnityEngine;
using System.Collections;

public class DisplayCurrentValue : MonoBehaviour 
{
	public Rigidbody rb;
	public LayerMask dieValueColliderLayer=-1;
	static public int currentValue = 0;
    static public bool move = false;
	RaycastHit hit;
	void Start(){
		rb = GetComponent<Rigidbody>();
	}
	void Update ()
	{
		if (Physics.Raycast (transform.position, Vector3.up, out hit, Mathf.Infinity, dieValueColliderLayer)) {
			if (Dicemove.move == true) {
                currentValue = hit.collider.GetComponent<DiceValue> ().value;
				Dicemove.move = false;
                move = true;
				Control.now_step = currentValue;
				Debug.Log (currentValue);
			}
		}
	} 
}
