using UnityEngine;
using System.Collections;

public class Char_motor: MonoBehaviour {
	
	
//	public GameObject Character; //РїРѕРєР° РЅРµ РёСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ
	public int Speed = 10; //СЃРєРѕСЂРѕСЃС‚СЊ РїРµСЂСЃРѕРЅР°Р¶Р°
	public int JumpHeight = 2;// РІС‹СЃРѕС‚Р° РїСЂС‹Р¶РєР°, РїРѕРєР° РЅРµ РёСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ
	public Ray GravChek;
	public int rost = 5;
	public bool CapsulCollide;
	public float in_walk;
	//Р°РЅРёРјР°С†РёРё
	public Animation Idle;
	public Animation Walk;
	public Animation Jump;
	//РґР»СЏ РѕС‚Р»Р°РґРєРё
	public Vector3 move;
	public RaycastHit[] hits;
	//РїСЂРёРІР°С‚РЅР°СЏ С‡Р°СЃС‚СЊ
	private int gravi;
	private Vector3 p1;
	private Vector3 p2;
	
	
	void Start () 
	{

	}
	
	void Update () 
	{
		if(in_walk!=0) Walking();
		if(Input.GetButton ("Jump"))
		{
			Jumping();
		};
		Gravity();
		
	}
	
	void Jumping()
	{
		if(!Physics.CapsuleCast (transform.position,(transform.position+Vector3.up*rost),1,Vector3.forward))
			{
				move.y+= JumpHeight;
				move.y*=Time.deltaTime;
				transform.Translate (move);
			}
	}
	
	void OnCollisionEnter()
	{
		Debug.Log ("Hit");
	}
	
	
	void Walking()
	{
		move.z=in_walk* Speed;
		move.z *= Time.deltaTime;
		move.y+=1*gravi/100; //РџСЂРёРїРѕРґРЅРёРјР°РµРј РїРµСЂСЃР°, С‡С‚РѕР± РЅРµ РїСЂРѕРІР°Р»РёР»СЃСЏ СЃРєРІРѕР·СЊ РїРѕР»
		move.y *=Time.deltaTime;
		transform.Translate (move);
		if(Walk!=null)
		{
			if(in_walk>0)
			{
				GetComponent<Animation>().Play ("Walk");
			}
			//else;
		}
	}
	
	void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position,3);
        Gizmos.DrawWireSphere(transform.position+Vector3.up*rost,3);
    }
	
	void Gravity()
	{
		CapsulCollide=Physics.CapsuleCast (transform.position,(transform.position+Vector3.up*rost),3,Vector3.up); //С‡С‚РѕР± РІРёРґРЅРѕ Р±С‹Р»Рѕ РІ РёРЅСЃРїРµРєС‚РѕСЂРµ
		//Vector3 down = transform.TransformDirection(Vector3.down);
		//if(!Physics.Raycast (transform.position,down,1))
		p1=(transform.position+Vector3.up*rost);
		p2=transform.position;
		RaycastHit[] col;
		col = Physics.CapsuleCastAll (p1,p2,3,Vector3.zero);
        if (col!=null)// РЅРµ СЂР°Р±РѕС‚Р°РµС‚!
		{
			gravi=Global.Gravitation;
			move.y -=1*gravi;
			move.y *=Time.deltaTime;
			transform.Translate (move);
		}
	}
}

