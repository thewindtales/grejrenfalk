using UnityEngine;
using System.Collections;

public class Char_motor: MonoBehaviour {
	
	
//	public GameObject Character; //пока не используется
	public int Speed = 10; //скорость персонажа
	public int JumpHeight = 2;// высота прыжка, пока не используется
	public Ray GravChek;
	//анимации
	public Animation Idle;
	public Animation Walk;
	public Animation Jump;
	//для отладки
	public Vector3 move;
	public Collision Grav;
	//приватная часть
	private int gravi;

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetAxis ("Horizontal")!=0) Walking();
		Gravity();
		
	}
	
	void OnCollisionEnter()
	{
		Debug.Log ("Hit");
	}
	
	
	void Walking()
	{
		move.z=Input.GetAxis ("Horizontal")* Speed;
		move.z *= Time.deltaTime;
		move.y+=1*gravi/100; //Приподнимаем перса, чтоб не провалился сквозь пол
		move.y *=Time.deltaTime;
		transform.Translate (move);
	}
	
	void Gravity()
	{
		Vector3 down = transform.TransformDirection(Vector3.down);
        if (!Physics.Raycast(transform.position, down, 1))
		{
			gravi=Global.Gravitation;
			move.y -=1*gravi;
			move.y *=Time.deltaTime;
			transform.Translate (move);
		}
	}
}

