using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCController : MonoBehaviour 
{
	private List<NPCPawnBase> m_NpcPawns = new List<NPCPawnBase>();
	
	// Use this for initialization
	void Start () 
	{
		m_NpcPawns.Add( new NPCMadMen() );
		foreach( NPCPawnBase pawn in m_NpcPawns )
		{
			pawn.Initialize();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach( NPCPawnBase pawn in m_NpcPawns )
		{
			pawn.Update();
		}
	}
}
