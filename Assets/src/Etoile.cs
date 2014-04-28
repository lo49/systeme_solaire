using UnityEngine;
using System.Collections;

public class Etoile : Astre {
	public Etoile(string n, Systeme s, Vector3 pos, Vector3 v, float m, float t) : base(n, s , pos, v, m, t) {
		astreReel.rigidbody.constraints = RigidbodyConstraints.FreezePosition; // Une étoile ne peut pas bouger
		astreVu.AddComponent<Light> ();
		astreVu.light.range = 100000F;
		astreVu.light.intensity = 0.8F;
		astreVu.renderer.material = Resources.Load<Material>("Solar");
	}
	


}

