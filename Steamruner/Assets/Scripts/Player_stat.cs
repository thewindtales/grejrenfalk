using UnityEngine;
using System.Collections;

public class Player_stat : Stat 
{
	public GameObject[] PickObjs;
	public GameObject RightArm;
	public GameObject Closes;
	public int PickUpDist = 7;
	private GameObject InHands;
	private float DisToItem;
	private bool EmptyHand=true;
	
	void Start () 
	{
		Self=transform;
		Closes = FindClosestItem();
	}

	void Update () 
	{
		PickObjs = GameObject.FindGameObjectsWithTag("Pickupble");
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(0,0,RectW,RectH),"Health " + CurHealth+"/"+MaxHealth,InGame);
		GUI.Label(new Rect(0,RectH,RectW,RectH),"Energy " + CurEnergy+"/"+MaxEnergy,InGame);
	}
	public void PickUp()
	{
		if(EmptyHand)
		{
			Closes = FindClosestItem();
			DisToItem = Vector3.Distance (Self.position, Closes.transform.position);
			if(DisToItem<PickUpDist)
			{
				Closes.transform.parent=RightArm.transform;
				Closes.transform.position=RightArm.transform.position;
				Closes.transform.rotation=Quaternion.Euler (0,0,0);
				Closes.GetComponent<Rigidbody>().isKinematic = true;
				Closes.GetComponent<Collider>().isTrigger = true;
				InHands = Closes;
				EmptyHand=false;
			}
		}		
	}
	public void Throw()
	{
		if(!EmptyHand)
		{
			InHands.transform.parent=null;
			Closes.GetComponent<Rigidbody>().isKinematic = false;
			Closes.GetComponent<Rigidbody>().AddForce (Vector3.forward * 1000);
			Closes.GetComponent<Collider>().isTrigger = false;
			EmptyHand = true;
		}
	}
	
	  GameObject FindClosestItem()
	{
		GameObject closest= null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach(GameObject Go in PickObjs)
		{
			Vector3 diff = Go.transform.position - position;
			float CurDis = diff.sqrMagnitude;
			if(CurDis<distance)
			{
				closest = Go;
                distance = CurDis;
			}
		}
		return closest;
	}
	
}
