﻿/// <summary>
/// Astre.cs
/// MODELE
/// créé par nicolas le 26/03
/// modifié par jonathan le 31/03
/// modifié par ...
/// </summary>

using UnityEngine;
using System.Collections;

public class Astre
{
	public string nom;
	public Systeme systeme;
	public Vector3 position;
	public Vector3 vitesseInit;
	public float masse;
	public float taille;
	public GameObject gameobject; // gameObject qui représente la planète (une sphère) dans la vue 3d

	public Astre(string n, Systeme s, Vector3 pos, Vector3 v, float m, float t)
	{
		nom = n;
		position = pos;
		vitesseInit = v;
		masse = m;
		taille = t;
		systeme = s;
		systeme.addAstre (this); // Lorsqu'on créé un Astre on l'ajoute à son système
		initGameObject ();
	}

	public void initGameObject() // j'aimerais que cette fonction soit dans un script AstreControler mais j'ai rencontré des probèmes ...
	{
		gameobject = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		gameobject.AddComponent("Rigidbody");
		gameobject.rigidbody.useGravity = false;
		gameobject.AddComponent ("SphereCollider");
		gameobject.rigidbody.transform.position = position;
		gameobject.rigidbody.transform.localScale = new Vector3(taille,taille,taille);
		gameobject.rigidbody.velocity = vitesseInit;
		gameobject.rigidbody.mass = masse;
		gameobject.rigidbody.name = nom;
	}
}