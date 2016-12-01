using UnityEngine;
using System.Collections;

[RequireComponent(typeof (SphereCollider))]

public class pincher : MonoBehaviour 
{
	
	public Transform RightPalm;
	public Transform LeftPalm;
	public Transform RightFoot;
	public Transform LeftFoot;
	public GameObject Victim;
	private int damage;
	private bool BattleMod;
	private int LeftArm = Animator.StringToHash("Base.Punch");
	private int RightArm = Animator.StringToHash("Base.Punch2");
	private int RightLeg = Animator.StringToHash("Base.Punch3");
	
	
	public GameObject Owner;
	private Transform Self;
	private Animator _anim;
	private AnimatorStateInfo AnInfo;


	void Start () 
	{
		Self=transform;
		Animator anima = Owner.GetComponent<Animator>();
		_anim= anima;
	}
	
	void OnTriggerEnter(Collider Vic)
	{
		
		Victim=Vic.gameObject;
		if(Vic.gameObject != Owner.gameObject)
		{
			if(BattleMod)
			{
				if(Victim)
				{
					if (Victim.GetComponent<Stat>())
					{
						Stat stat = Victim.GetComponent<Stat>();
						stat.HealthInjury(damage);
						Debug.Log ("у " + Vic.gameObject.name + " отнимается " + damage + " здоровья");
						BattleMod = false;
					}
				}
			}
		}
	}
	

	void Update () 
	{
		AnInfo = _anim.GetCurrentAnimatorStateInfo(0);
		
		if(AnInfo.nameHash == LeftArm)
		{
			Self.position = LeftPalm.position;
			BattleMod = true;
			damage = 10;
		}
		if(AnInfo.nameHash == RightLeg)
		{
			Self.position = RightFoot.position;
			BattleMod = true;
			damage = 30;
		}
		if(AnInfo.nameHash == RightArm)
		{
			Self.position = RightPalm.position;
			BattleMod = true;
			damage = 10;
		}
		if(!((AnInfo.nameHash == RightArm)||(AnInfo.nameHash == LeftArm)||(AnInfo.nameHash == RightLeg)))
		{
			BattleMod=false;
			Self.position=Vector3.zero;
		}
		
	}
	
}
