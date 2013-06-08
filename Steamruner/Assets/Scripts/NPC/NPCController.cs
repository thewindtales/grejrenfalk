using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCController : MonoBehaviour 
{
	private List<NPCPawnBase> m_NpcPawns = new List<NPCPawnBase>();
	
	public List<NPCPawnBase> NPCInScene
	{
		get
		{
			return m_NpcPawns;
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		m_NpcPawns.Add( new NPCMadMen() );
		m_NpcPawns.Add( new NPCMadMen() );
		m_NpcPawns.Add( new NPCMadMen() );
		int pos = 0;
		foreach( NPCPawnBase pawn in m_NpcPawns )
		{
			pawn.Initialize();
			pos += 30;
			pawn.GetPawnGameObject().transform.position = new Vector3( 0, 10, pos );
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
