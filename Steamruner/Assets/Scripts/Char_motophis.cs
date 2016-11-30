using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Char_motophis : MonoBehaviour 
{

	public float in_walk;
	

	public Transform Self;
	public bool Left = false;

	
	

	// Use this for initialization
	void Start () 
	{
		Self=transform;
		GetComponent<Rigidbody>().freezeRotation=true;//РќРµС‡РµРіРѕ РїР°РґР°С‚СЊ РЅР° Р±РѕРє
	}
	
	void Update () 
	{		
		if(in_walk!=0)
			Walking ();
	}

	
	void Walking()
	{
		if(in_walk>0)
		{
			if(Left)
			{
				//Р•СЃР»Рё СЃРјРѕС‚СЂРµР»Рё РЅР°Р»РµРІРѕ, С‚Рѕ С‚РµРїРµСЂСЊ РїРѕРІРѕСЂР°С‡РёРІР°РµРјСЃСЏ РЅР°РїСЂР°РІРѕ
				Left=false;
				Self.rotation=Quaternion.Euler (new Vector3(0,0,0));
			}
			Vector3 forw =new Vector3 (0,GetComponent<Rigidbody>().velocity.y,in_walk);
			GetComponent<Rigidbody>().velocity=forw;
		}
		if(in_walk<0)
		{
			if (!Left)
			{
				Left=true;
				Self.rotation=Quaternion.Euler(new Vector3(0,180,0));
			}
			Vector3 back =new Vector3 (0,GetComponent<Rigidbody>().velocity.y,in_walk);
			GetComponent<Rigidbody>().velocity= back;
		}
	}
	
}
