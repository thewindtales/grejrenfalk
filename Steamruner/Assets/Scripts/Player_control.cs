using UnityEngine;
using System.Collections;

public class Player_control : MonoBehaviour 
{
	public float movein;

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
		
		Char_motophis other=gameObject.GetComponent<Char_motophis>();
		other.in_walk=movein;
	}
}
