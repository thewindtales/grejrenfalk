using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]

public class Camera_follow : MonoBehaviour 
{
	public Transform player;
	public int Height=10;
	public int Range=10;
	public bool Smooth=false;
	public float Damping=6;
	
	private Transform Self;
	private Vector3 cam_pos;
	private Quaternion Rot;

	void Start () 
	{
		Self=transform;
	}
	
	void Update () 
	{
		if(player)
		{
			if(Smooth)
			{
				Rot = Quaternion.LookRotation(player.position-Self.position);
				Self.rotation=Quaternion.Slerp (Self.rotation,Rot,Time.deltaTime*Damping);
			}
			else Self.LookAt (player);
			
			cam_pos=new Vector3(Range,Height,0);
			transform.position=player.position+cam_pos;
		}
	}
}
