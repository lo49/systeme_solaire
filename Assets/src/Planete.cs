using UnityEngine;
using System.Collections;

public class Planete : Astre {
	private float posIX;
	private float posIY;
	private float posIZ;
	private float vitIX;
	private float vitIY;
	private float vitIZ;
	private string composition;

	public Planete(string n, Systeme s, Vector3 pos, Vector3 v, float m, float t) : base(n, s , pos, v, m, t) {
	}

	public float getPosIX(){
		return posIX;
	}
	public float getPosIY(){
		return posIY;
	}
	public float getPosIZ(){
		return posIZ;
	}
	public float getVitIX(){
		return vitIX;
	}
	public float getVitIY(){
		return vitIY;
	}
	public float getVitIZ(){
		return vitIZ
	}
	public string getComposition(){
		return composition;
	}

}
