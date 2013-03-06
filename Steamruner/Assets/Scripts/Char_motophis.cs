using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Char_motophis : MonoBehaviour 
{
	public int Speed = 10; //скорость персонажа
	public int JumpHeight = 20;// высота прыжка, пока не используется
	public bool jump;
	public float in_walk;
	
	//анимации
	public AnimationClip Idle;
	public AnimationClip Walk;
	public AnimationClip Jump;
	public Transform Self;
	public bool Left = false;
	
	private Vector3 JumpVect;
	private bool OnGround;
	
	
	

	// Use this for initialization
	void Start () 
	{
		Self=transform;
		rigidbody.freezeRotation=true;//Нечего падать на бок
		animation.AddClip (Idle,"Idle");
		animation.AddClip (Walk,"Walk");
	}
	
	// Update is called once per frame
	void Update () 
	{
		animation.CrossFade ("Idle");
		
		if(in_walk!=0)
			Walking ();
		if(jump&&OnGround)
			Jumping ();
	}
//	void 
	 void OnCollisionStay(Collision collision) 
	{
		OnGround=true;
	}
	 void OnCollisionExit(Collision collision) 
	{
        OnGround=false;
    }
	
	void Walking()
	{
		animation.CrossFade ("Walk");
		if(in_walk>0)
		{
			if(Left)
			{
				//Если смотрели налево, то теперь поворачиваемся направо
				Left=false;
				Self.rotation=Quaternion.Euler (new Vector3(0,0,0));
			}
			Vector3 forw =new Vector3 (0,rigidbody.velocity.y,in_walk*Speed);
			rigidbody.velocity=forw;
			animation.CrossFade ("Walk");
		}
		if(in_walk<0)
		{
			if (!Left)
			{
				Left=true;
				Self.rotation=Quaternion.Euler(new Vector3(0,180,0));
			}
			Vector3 back =new Vector3 (0,rigidbody.velocity.y,in_walk*Speed);
			rigidbody.velocity= back;
		}
	}
	
	void Jumping()
	{
		JumpVect =new Vector3(rigidbody.velocity.x,JumpHeight,rigidbody.velocity.z);
		rigidbody.velocity= JumpVect;
	}
}
