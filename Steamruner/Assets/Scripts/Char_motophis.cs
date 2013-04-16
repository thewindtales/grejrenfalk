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
		rigidbody.freezeRotation=true;//Нечего падать на бок
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
				//Если смотрели налево, то теперь поворачиваемся направо
				Left=false;
				Self.rotation=Quaternion.Euler (new Vector3(0,0,0));
			}
			Vector3 forw =new Vector3 (0,rigidbody.velocity.y,in_walk);
			rigidbody.velocity=forw;
		}
		if(in_walk<0)
		{
			if (!Left)
			{
				Left=true;
				Self.rotation=Quaternion.Euler(new Vector3(0,180,0));
			}
			Vector3 back =new Vector3 (0,rigidbody.velocity.y,in_walk);
			rigidbody.velocity= back;
		}
	}
	
}
