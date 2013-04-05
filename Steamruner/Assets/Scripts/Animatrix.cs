using UnityEngine;
using System.Collections;


[RequireComponent(typeof (Animator))]

public class Animatrix : MonoBehaviour 
{
	private Animator anim;

	void Start () 
	{
		anim=GetComponent<Animator>();
	}
	

	void Update () 
	{
		float h= Input.GetAxis ("Horizontal");
		if (h!=0)
		{
			anim.SetBool("Walk",true);
		}
		else
		{
			anim.SetBool ("Walk", false);
		}
		if(Input.GetButton("Shift"))
		{
			anim.SetBool ("Run",true);
		}
		else
		{
			anim.SetBool ("Run",false);
		}
	}
}
