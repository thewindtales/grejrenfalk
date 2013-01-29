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
		if(GUI.Button(new Rect(Screen.width/2,Screen.height/2,NewGame.width,NewGame.height),NewGame))
		{};
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
