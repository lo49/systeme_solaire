using UnityEngine;
using System.Collections;

public class GetAstre : MonoBehaviour {

	private Astre astre;


	public Astre getAstre()
	{
		return astre;
	}

	public void setAstre(Astre a)
	{
		astre = a;
	}
}
