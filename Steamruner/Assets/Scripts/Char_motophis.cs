using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Player_control))]

public class Char_motophis : MonoBehaviour 
{
	public int Speed = 10; //скорость персонажа
	public int JumpHeight = 2;// высота прыжка, пока не используется
	public int rost = 5;
	public bool CapsulCollide;
	public float in_walk;
	//анимации
	public Animation Idle;
	public Animation Walk;
	public Animation Jump;
	public Transform Self;

	// Use this for initialization
	void Start () 
	{
		Self=transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(in_walk!=0)
			Walking ();
	}
	
	void Walking()
	{
		if(in_walk>0)
		{
			 rigidbody.velocity = Vector3.forward*10;
			//rigidbody.AddForce (Vector3.forward*10);
		}
		if(in_walk==0)
		{
			rigidbody.velocity=Vector3.zero;
		}
		if(in_walk<0)
		{
			rigidbody.velocity=Vector3.back*10;
		}
	}
	
	void Jumping()
	{
		Debug.Log ("Jump");
	}
}
