using UnityEngine;
using System.Collections;

public class SetMaxAuglarVelocity: MonoBehaviour {
	public float maxAugularVelocity = 7.0f;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().maxAngularVelocity = maxAugularVelocity;
	}
}
