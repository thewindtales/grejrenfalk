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
	

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown (KeyCode.E))
		{
			if(recharge)
				Ability ();
		}
	}
	void HealthUp(int val)
	{
		CurHealth+=val;
		if(CurHealth>MaxHealth)
		{
			CurHealth=MaxHealth;
		}
	}
	
	void HealthInjury(int val)
	{
		CurHealth-= val;
		if(CurHealth<=0)
			Death();
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
	void Death(){}
	
	
	
	void OnGUI()
	{
		GUI.Label(new Rect(0,0,RectW,RectH),"Health " + CurHealth+"/"+MaxHealth,InGame);
		GUI.Label(new Rect(0,RectH,RectW,RectH),"Energy " + CurEnergy+"/"+MaxEnergy,InGame);
	}
}
