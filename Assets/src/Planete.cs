using UnityEngine;
using System.Collections;

public class Planete : Astre
{
	private string composition;

	public Planete(string n, Systeme s, Vector3 pos, Vector3 v, float m, float t, string tex)
		: base(n, s , pos, v, m, t, tex)
	{
		s.addPlanete (this);
	}

	// ACCESSEURS
	public string getComposition()
	{
		return composition;
	}
	
	// MUTATEURS
	public void setComposition(string c)
	{
		composition = c;
	}
}
