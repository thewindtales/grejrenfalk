using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]

public class Camera_follow : MonoBehaviour 
{
	public Transform player;
	public int Height=10;
	public int Range=10;
	private Vector3 cam_pos;

	void Start () 
	{
		//cam_pos=new Vector3(Range,Height,0);
	}
	
	void Update () 
	{
		cam_pos=new Vector3(Range,Height,0);
		transform.position=player.position+cam_pos;
	}
}
