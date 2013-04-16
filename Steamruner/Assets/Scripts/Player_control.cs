using UnityEngine;
using System.Collections;

public class Player_control : MonoBehaviour 
{
	public float movein;
	private bool _Jump;
	private bool _Sprint;
	private bool _Punch;
	private Transform self;
	

	// Use this for initialization
	void Start () 
	{
		self= transform;
	}
	
	void Update () 
	{
		if(Input.GetAxis ("Horizontal")!=0)
		{
			movein=Input.GetAxis ("Horizontal"); 
		}
		else movein=0;
		
		
		//Прыжок
		if(Input.GetButton ("Jump"))
		{
			_Jump = true;
		}
		else _Jump=false;
		
		//Спринт
		if(Input.GetButton("Shift"))
		{
			_Sprint=true;
		}
		else
		_Sprint=false;
		
		//Удар
		if(Input.GetButton("Fire1"))
		{
			_Punch=true;
		}
		else _Punch=false;
		
		
		
		
		Char_motophis motor = self.GetComponent<Char_motophis>();
		motor.in_walk=movein;
		
		Animatrix InAnim = self.GetComponent<Animatrix>();
		InAnim.InWalk=movein;
		InAnim.Sprint=_Sprint;
		InAnim.Jump=_Jump;
		InAnim.Punch=_Punch;
		
	}
}
