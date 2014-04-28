/// <summary>
/// Main.cs
/// CONTROLER (MAIN)
/// créé par nicolas le 26/03
/// modifié par jonathan le 31/03
/// modifié par ...
/// </summary>

using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour // Main attaché à un objet vide pour etre appelé 
{
	// Use this for initialization
	void Start ()
	{
		Systeme s1 = new Systeme ("système solaire");
		Etoile soleil = new Etoile ("soleil", s1, new Vector3 (0f, 0f, 0f), new Vector3 (0f, 0f, 0f), 1989100f, 6.96342F);
		Planete Mercure = new Planete ("Mercure", s1, new Vector3 (-210.5027f, -34.92726f, -664.0743f), new Vector3 (36.653516f, -4.368085f, -12.28817f), 0.3302F, 0.024397F);
		Planete Venus = new Planete ("Venus", s1, new Vector3 (-1075.055f, 61.59186f, -33.68777f), new Vector3 (0.8898920f, -0.5319014f, -35.15918f), 4.8685F, 0.060518F);
		Planete Terre = new Planete ("Terre", s1, new Vector3 (-252.1284f, -0.006162688f, 1449.276f), new Vector3 (-29.83977f, 0.00006182963f, -5.208023f), 5.9736F, 0.06371F);
		Planete Lune = new Planete ("Lune", s1, new Vector3 (-255.30458f, 0.3593945f, 1446.8565f), new Vector3 (-29.27885f, -0.0015922926f, -6.0078587f), 0.073477F, 0.017367F);
		Planete Mars = new Planete ("Mars", s1, new Vector3 (2079.951f, -51.78748f, -31.413225f), new Vector3 (1.294807f, 0.5190147f, 26.29442f), 0.64185F, 0.033895F);
		Planete Jupiter = new Planete ("Jupiter", s1, new Vector3 (5989.086f, -152.3269f, 4391.234f), new Vector3 (-7.9014697f, 0.1306501f, 11.16227f), 1898.6F, 0.69911F);
		Planete Saturne = new Planete ("Saturne", s1, new Vector3 (9587.0567f, -552.2077f, 9825.658f), new Vector3 (-7.429892f, 0.1781269f, 6.738101f), 568.46F, 0.58232F);
		Planete Uranus = new Planete ("Uranus", s1, new Vector3 (21587.76f, -356.2269f, -20548.24f), new Vector3 (4.6374966f, -0.042899693f, 4.6272206f), 86.810F, 0.25361F);
		Planete Neptune = new Planete ("Neptune", s1, new Vector3 (25148.559f, 190.404f, -37388.49f), new Vector3 (4.466067f, -0.1660625f, 3.0763957f), 102.43F, 0.24622F);
		//Astre Pluton = new Astre ("Pluton", s1, new Vector3 (59064.51F, 0F, 0F), new Vector3 (0F, 0F, 29.783F), 0.01314F, 0.01151);
	}
	

}
