using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public string GameName = "SteamRa";
	public AudioClip Music;
	public Texture2D NewGame;
	public Texture2D Options;
	public Texture2D Exit;
	public Texture2D Cursor;
	public GUIStyle Guistyle_Menu;
	
	
	void OnGUI()
	{
		GUI.Label (new Rect(Screen.width/2,20, 100, 20), GameName ,Guistyle_Menu);
		if(GUI.Button(new Rect(Screen.width/2-NewGame.width/2,Screen.height/2-NewGame.height,NewGame.width,NewGame.height),NewGame))
		{
			Debug.Log ("New game selected");
			Application.LoadLevel ("GameScene1a");
		};
		if(GUI.Button (new Rect(Screen.width/2-Options.width/2,Screen.height/2,Options.width,Options.height),Options))
		{
			Debug.Log ("Go to options");
			
		}
		if(GUI.Button (new Rect(Screen.width/2-Exit.width/2,Screen.height/2+Exit.height,Exit.width,Exit.height),Exit))
		{
			Debug.Log ("Exit Game");
			Application.Quit();
			
		}
		CursorTrack ();
		
		
	}

	void Start () 
	{
		
	}

	void Update () 
	{
		
	}
	
	void CursorTrack()
	{
		Vector3 MousePos = Input.mousePosition;
		float sizeCursor = Cursor.height;
		Rect CursorRect = new Rect(MousePos.x, Screen.height-MousePos.y,sizeCursor,sizeCursor);
		GUI.Label (CursorRect,Cursor);
		
	}
}
