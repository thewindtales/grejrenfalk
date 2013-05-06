using UnityEngine;
using System.Collections;


[RequireComponent(typeof (Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Animatrix : MonoBehaviour 
{
	private Animator anim;
	
	//публятня
	
	public bool Jump=false;
	public bool Sprint = false;
	public bool Punch = false;
	public float InWalk = 0;
	public float in_walk;
	

	public Transform Self;
	public bool Left = false;
	

	void Start () 
	{
		anim=GetComponent<Animator>();
		Self=transform;
		rigidbody.freezeRotation=true;//Нечего падать на бок
	}
	

	void Update () 
	{
		if(InWalk!=0)
			Walking ();
		
		if (InWalk!=0)
		{
			anim.SetBool("Walk",true);
		}
		else
		{
			anim.SetBool ("Walk", false);
		}
		if(Sprint)
		{
			anim.SetBool ("Run",true);
		}
		else
		{
			anim.SetBool ("Run",false);
		}
		if(Jump)
		{anim.SetBool ("Jump",true);}
		else
		{
			anim.SetBool("Jump",false);
		}
		if(Punch)
		{anim.SetBool ("Punch",true);}
		else
		{
			anim.SetBool ("Punch",false);
		}
	}
	void Walking()
	{
		if(InWalk>0)
		{
			if(Left)
			{
				//Если смотрели налево, то теперь поворачиваемся направо
				Left=false;
				Self.rotation=Quaternion.Euler (new Vector3(0,0,0));
			}
			Vector3 forw =new Vector3 (0,rigidbody.velocity.y,InWalk);
			rigidbody.velocity=forw;
		}
		if(InWalk<0)
		{
			if (!Left)
			{
				Left=true;
				Self.rotation=Quaternion.Euler(new Vector3(0,180,0));
			}
			Vector3 back =new Vector3 (0,rigidbody.velocity.y,InWalk);
			rigidbody.velocity= back;
		}
	}
}
