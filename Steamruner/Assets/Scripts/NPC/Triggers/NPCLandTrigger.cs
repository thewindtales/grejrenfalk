using UnityEngine;
using System.Collections;

[RequireComponent(typeof (GameObject))]
public class NPCLandTrigger : MonoBehaviour {
	
	public GameObject m_NPCControllerGameobject;
	private NPCController m_NPCController;
	
	
	void Start () 
	{
		m_NPCController = m_NPCControllerGameobject.GetComponent<NPCController>();
		if( !m_NPCController )
		{
			Debug.LogError( "NPCLandTrigger: Can't find NPCController" );
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnTriggerEnter( Collider other )
	{
		foreach( NPCPawnBase pawn in m_NPCController.NPCInScene )
		{
			if( pawn.GetPawnGameObject() == other.gameObject )
			{
				pawn.SetCurrentState( NPCPawnBase.NPCBaseStates.NPCS_IDLE );
			}
		}
	}
	
	void OnTriggerExit( Collider other )
	{
 		foreach( NPCPawnBase pawn in m_NPCController.NPCInScene )
		{
			if( pawn.GetPawnGameObject() == other.gameObject )
			{
				pawn.SetCurrentState( NPCPawnBase.NPCBaseStates.NPCS_FLYING );
			}
		}
	}
}
