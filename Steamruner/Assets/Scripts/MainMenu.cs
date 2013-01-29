using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public Texture2D NewGame;
	public Texture2D Options;
	public Texture2D Exit;
	public Texture2D Cursor;
	public GUIStyle Guistyle_Menu;
	
	
	void OnGUI()
	{
		GUI.Label (new Rect(Screen.width/2,20, 100, 20), "Our Game",Guistyle_Menu);
		if(GUI.Button(new Rect(Screen.width/2,Screen.height/2-NewGame.height,NewGame.width,NewGame.height),NewGame))
		{
			Debug.Log ("New game selected");
			Application.LoadLevel ("GameScene1");
		};
		if(GUI.Button (new Rect(Screen.width/2,Screen.height/2,Options.width,Options.height),Options))
		{
			Debug.Log ("Go to options");
			
		}
		if(GUI.Button (new Rect(Screen.width/2,Screen.height/2+Exit.height,Exit.width,Exit.height),Exit))
		{
			Debug.Log ("Exit Game");
			Application.Quit();
			
		}
		CursorTrack ();
		
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void CursorTrack()
	{
		Vector3 MousePos = Input.mousePosition;
		float sizeCursor = Cursor.height;
		Rect CursorRect = new Rect(MousePos.x, Screen.height-MousePos.y,sizeCursor,sizeCursor);
		GUI.Label (CursorRect,Cursor);
		
	}
}
