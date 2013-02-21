using UnityEngine;
using System.Collections;

public class Player_control : MonoBehaviour 
{
	public float movein;
	private bool Jump;

	// Use this for initialization
	void Start () 
	{

	}
	
	void Update () 
	{
		if(Input.GetAxis ("Horizontal")!=0)
		{
			movein=Input.GetAxis ("Horizontal"); 
		}
		else movein=0;
		
		if(Input.GetButtonDown ("Jump"))
		{
			Jump=true;
		}
		else
			Jump = false;
		
		Char_motophis motor=gameObject.GetComponent<Char_motophis>();
		motor.in_walk=movein;
		motor.jump=Jump;
	}
}
