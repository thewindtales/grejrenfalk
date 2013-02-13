using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour
{
	public int gravitat = 9; //ввод гравитации
	public static int Gravitation = 9; // вывод гравитации
	

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		Gravitation=gravitat; //присвоение гравитации для вывода, тут она на случай, если нам приспичит играться с гравитацией геймплейно	
	}
}
