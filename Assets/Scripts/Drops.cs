using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Drops : MonoBehaviour {
	public int speed = 25;

	private int num1 = 0;
	private int num2 = 0;
	public int answer = 0;
	public string question = "";

	public GameObject questionText; //GUI text to display the question
	public Image sprite;
	public bool isAnswered = false; //Check if this drop has been answered

	// Use this for initialization
	void Start () {
		isAnswered = false;
		generateQuestion ();
		sprite = this.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isAnswered)
			speed = (int)(1.5f * speed);

		transform.Translate (new Vector3 (0, -1, 0) * speed * Time.deltaTime); 

		if (transform.localPosition.y < -140) {
			this.speed = 0;
			transform.localPosition = new Vector3(transform.localPosition.x, -140,0);
			//attachCollider();
			if(!isAnswered)
			{
				destroyThis(true);
				GameHandler.instance.healthBar.value -= 2; 
				GameHandler.instance.playerHealth = (int)GameHandler.instance.healthBar.value;
				GameHandler.instance.player.GetComponent<Player>().showSpeechBubble();
			}
		}
	}

	void generateQuestion()
	{
		//min, max and range increase as the gamem progresses
		int min = GameHandler.instance.min;
		int max = min + GameHandler.instance.range;
		Debug.Log ("min: "+min+" max: "+max);
		num1 = Random.Range (min, max);
		num2 = Random.Range (min, max);

		int type = Random.Range (0,2);

		if (type == 0) // //addition
		{
			question = num1.ToString()+"+"+num2.ToString();
			answer = num1 + num2;
		}else //subtraction
		{
			if(num1 > num2)
			{
				question = num1.ToString()+"-"+num2.ToString();
				answer = num1 - num2;
			}else
			{
				question = num2.ToString()+"-"+num1.ToString();
				answer = num2 - num1;
			}

		}

		Debug.Log (""+question+" = "+answer);
		GetComponentInChildren<Text> ().text = question;
	}

	//Called when either answered correctly or hits the ground unsolved. 
	//If answerd, pop from GameHandler.dropList and push into GameHandler.foodList
	public void destroyThis(bool destroy)
	{

		if(GameHandler.instance.dropList.Contains(this.gameObject))
		{
			if(isAnswered)
			{
				GameHandler.instance.foodList.Add(this.gameObject);
			}
			GameHandler.instance.dropList.Remove(this.gameObject);
			Debug.Log("food scale "+gameObject.transform.localScale.x);
			gameObject.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		}

		if (destroy) {
			Destroy (this.gameObject);
		}

	}

	//not used
	/*
	public void attachCollider()
	{
		BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D> ();
		collider.size = gameObject.GetComponent<RectTransform> ().sizeDelta;
		collider.isTrigger = true;
		this.tag = "food";
	}*/
}
