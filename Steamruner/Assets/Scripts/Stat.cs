using UnityEngine;
using System.Collections;

public class Stat : MonoBehaviour 
{
	public int MaxHealth = 100;
	public int CurHealth = 100;
	public int MaxEnergy = 100;
	public int CurEnergy = 100;
	public GUIStyle InGame;
	public int RectH=20;
	public int RectW=100;
	public int rech = 10;

	public bool recharge;
	
	public Transform Self;
	
	public GameObject RagDoll;



	void Start () 
	{
		Self=transform;
	}


	void Update () 
	{
		
	}
	void HealthUp(int val)
	{
		CurHealth+=val;
		if(CurHealth>MaxHealth)
		{
			CurHealth=MaxHealth;
		}
	}

	public void HealthInjury(int val)
	{
		CurHealth-= val;
		if(CurHealth<=0)
		{
			CurHealth=0;
			Death();
		}
	}
	void EnergyUp (int val)
	{
		CurEnergy +=val;
		if(CurEnergy>MaxEnergy)
		{
			CurEnergy=MaxEnergy;
		}
	}

	void EnergyLow(int val)
	{
		CurEnergy-=val;
		if(CurEnergy<=0)
			CurEnergy=0;
	}

	void Ability()
	{
		recharge=false;
		EnergyLow (20);



	}
	void Death()
	{
		Debug.Log (Self.gameObject.name + "Умер");
		Instantiate (RagDoll,Self.position,Self.rotation);
		Destroy(Self.gameObject);
	}
}