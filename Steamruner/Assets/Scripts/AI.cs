using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	
	public float DistanseAgr=20;
	public float AttakDist=2;
	public GameObject Player;
	private Transform _Self;
	private float _CurDist;
	private float _Move;
	private float _DirMod;
	private float _temp;
	private Vector3 _DirSub;
	void Start () 
	{
		_Self = transform;
	}
	
	void Update () 
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		if(Player)
		{
			_CurDist = Vector3.Distance (Player.transform.position ,_Self.position);
			_DirSub=Player.transform.position - _Self.position;
			if(_DirSub.z<0)
			{
				_DirMod=-1;
			}
			if(_DirSub.z>0)
			{
				_DirMod = 1;
			}
			if(_CurDist<DistanseAgr&&AttakDist<_CurDist)
			{
				Animatrix InAnim = _Self.GetComponent<Animatrix>();
				InAnim.InWalk=1*_DirMod;			
			}
			else
			{
				Animatrix InAnim = _Self.GetComponent<Animatrix>();
				InAnim.InWalk=0;
			}
			if(_CurDist<AttakDist)
			{
				Animatrix InAnim = _Self.GetComponent<Animatrix>();
				InAnim.Punch=true;
			}
			else
			{
				Animatrix InAnim = _Self.GetComponent<Animatrix>();
				InAnim.Punch=false;
			}
		}
		else
		{
			Animatrix Inanim =_Self.GetComponent<Animatrix>();
			Inanim.Punch=false;
		}
	}
}
