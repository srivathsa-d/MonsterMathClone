using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {

	#region Singleton
	
	private static GameHandler _instance;
	
	public static GameHandler instance
	{
		get
		{
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<GameHandler>();
			return _instance;
		}
	}
	#endregion
	


	public TimeScript timeManager;
	public List<GameObject> dropList; // List of crates dropped
	public List<GameObject> foodList; // List of food available 
	public int inputValue;
	public GameObject inputText; //GUI text to show player input
	private GameObject currentDrop; //The current active question that needs to be answered

	//Difficulty parameters
	public int min = 0;
	public int range = 4;
	public int correctAnswerCount = 0;

	//Player health
	public int playerHealth = 10;
	public Slider healthBar;

	//Player
	public GameObject player;

	// Use this for initialization
	void Start () {

		timeManager = this.GetComponent<TimeScript> ();
		healthBar = GameObject.Find ("HealthBar").GetComponent<Slider>();
		arrangeButtons ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (timeManager.TimeInterval (5)) {
			Debug.Log ("instantiating a drop");
			GameObject drop = Instantiate (Resources.Load("drop")) as GameObject;
			drop.transform.SetParent(GameObject.Find("Canvas").transform);
			int x = Random.Range(-400,400);
			drop.transform.localPosition = new Vector3(x,600,0);
			dropList.Add(drop);


		}
	}
	//create 10 buttons at the bottom of the screen
	void arrangeButtons()
	{
		int buttonId = 0;

		for (int i = -5; i < 5; i++) {
			GameObject btn = Instantiate (Resources.Load("Button")) as GameObject;
			btn.transform.SetParent(GameObject.Find("Canvas").transform);
			int x = (i * 120) + (i*8);
			btn.GetComponentInChildren<Text>().text = buttonId.ToString();
			btn.transform.localPosition = new Vector3(x,-360,0);
			btn.transform.localScale = new Vector3(1,1,1);
			buttonId++;
		}
	}

	//Get the current expected answer
	public int getCurrentDrop()
	{
		return dropList [0].GetComponent<Drops>().answer;
	}

	//Check game over condition
	public void checkGameOver()
	{
		if (healthBar.value == 0) {
			Debug.Log("Game Over");
			Application.LoadLevel(2);
		}
	}
}
