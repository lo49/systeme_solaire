using UnityEngine;
using System.Collections;

public class Etoile : Astre {
	public Etoile(string n, Systeme s, Vector3 pos, Vector3 v, float m, float t) : base(n, s , pos, v, m, t) {
		gameobject.rigidbody.isKinematic = true;	//une étoile ne peut pas bouger
	}
	


}

