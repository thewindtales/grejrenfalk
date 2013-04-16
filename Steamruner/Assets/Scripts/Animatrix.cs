using UnityEngine;
using System.Collections;


[RequireComponent(typeof (Animator))]

public class Animatrix : MonoBehaviour 
{
	private Animator anim;
	
	//публятня
	
	public bool Jump=false;
	public bool Sprint = false;
	public bool Punch = false;
	public float InWalk = 0;
	

	void Start () 
	{
		anim=GetComponent<Animator>();
	}
	

	void Update () 
	{
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
}
