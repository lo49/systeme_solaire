using UnityEngine;
using System.Collections;

public class Etoile : Astre
{
	private float temperature;
	private float luminosite;

	public Etoile(string n, Systeme s, Vector3 pos, Vector3 v, float m, float t, string tex)
		: base(n, s , pos, v, m, t, tex)
	{
		temperature = 0f;
		luminosite = 0.8f;
		astreReel.rigidbody.constraints = RigidbodyConstraints.FreezePosition; // Une étoile ne peut pas bouger
		astreVu.AddComponent<Light> ();
		astreVu.light.range = 100000000000F;
		astreVu.light.intensity = luminosite;
		s.addEtoile (this);
	}

	// ACCESSEURS
	public float getTemperature()
	{
		return temperature;
	}
	public float getLuminosite()
	{
		return temperature;
	}
}

