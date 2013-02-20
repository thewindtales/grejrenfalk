using UnityEngine;
using System.Collections;

public class Char_motor: MonoBehaviour {
	
	
//	public GameObject Character; //пока не используется
	public int Speed = 10; //скорость персонажа
	public int JumpHeight = 2;// высота прыжка, пока не используется
	public Ray GravChek;
	public int rost = 5;
	public bool CapsulCollide;
	public float in_walk;
	//анимации
	public Animation Idle;
	public Animation Walk;
	public Animation Jump;
	//для отладки
	public Vector3 move;
	public RaycastHit[] hits;
	//приватная часть
	private int gravi;
	private Vector3 p1;
	private Vector3 p2;
	
	
	void Start () 
	{

	}
	
	void Update () 
	{
		if(in_walk!=0) Walking();
		if(Input.GetButton ("Jump"))
		{
			Jumping();
		};
		Gravity();
		
	}
	
	void Jumping()
	{
		if(!Physics.CapsuleCast (transform.position,(transform.position+Vector3.up*rost),1,Vector3.forward))
			{
				move.y+= JumpHeight;
				move.y*=Time.deltaTime;
				transform.Translate (move);
			}
	}
	
	void OnCollisionEnter()
	{
		Debug.Log ("Hit");
	}
	
	
	void Walking()
	{
		move.z=in_walk* Speed;
		move.z *= Time.deltaTime;
		move.y+=1*gravi/100; //Приподнимаем перса, чтоб не провалился сквозь пол
		move.y *=Time.deltaTime;
		transform.Translate (move);
		if(Walk!=null)
		{
			if(in_walk>0)
			{
				animation.Play ("Walk");
			}
			//else;
		}
	}
	
	void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position,3);
        Gizmos.DrawWireSphere(transform.position+Vector3.up*rost,3);
    }
	
	void Gravity()
	{
		CapsulCollide=Physics.CapsuleCast (transform.position,(transform.position+Vector3.up*rost),3,Vector3.up); //чтоб видно было в инспекторе
		//Vector3 down = transform.TransformDirection(Vector3.down);
		//if(!Physics.Raycast (transform.position,down,1))
		p1=(transform.position+Vector3.up*rost);
		p2=transform.position;
		RaycastHit[] col;
		col = Physics.CapsuleCastAll (p1,p2,3,Vector3.zero);
        if (col!=null)// не работает!
		{
			gravi=Global.Gravitation;
			move.y -=1*gravi;
			move.y *=Time.deltaTime;
			transform.Translate (move);
		}
	}
}

