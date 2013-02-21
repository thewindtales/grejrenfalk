using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Player_control))]

public class Char_motophis : MonoBehaviour 
{
	public int Speed = 10; //скорость персонажа
	public int JumpHeight = 2;// высота прыжка, пока не используется
	public bool jump;
	public float in_walk;
	public bool OnGround;
	//анимации
	public Animation Idle;
	public Animation Walk;
	public Animation Jump;
	public Transform Self;
	
	public Vector3 DebuRigid;
	
	private Vector3 JumpVect;
	
	
	
	

	// Use this for initialization
	void Start () 
	{
		Self=transform;
		rigidbody.freezeRotation=true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
		if(in_walk!=0)
			Walking ();
		if(jump&&OnGround)
			Jumping ();
		DebuRigid=rigidbody.velocity;
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
		if(in_walk>0)
		{
			Vector3 forw =new Vector3 (0,rigidbody.velocity.y,in_walk*Speed);
			rigidbody.velocity=forw;
		}
		if(in_walk<0)
		{
			Vector3 back =new Vector3 (0,rigidbody.velocity.y,in_walk*Speed);
			rigidbody.velocity= back;
		}
	}
	
	void Jumping()
	{
		JumpVect =new Vector3(rigidbody.velocity.x,JumpHeight,rigidbody.velocity.z);
		//JumpVect+=rigidbody.velocity;
		rigidbody.velocity= JumpVect;
	}
}
