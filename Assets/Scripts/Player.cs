using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	GameObject currentFood;
	public GameObject speechBubble;

	int speed = 100;

	// Use this for initialization
	void Start () {
		currentFood = null;
		speechBubble = Instantiate (Resources.Load ("Speech")) as GameObject;
		speechBubble.transform.parent = this.transform;
		speechBubble.transform.localPosition = new Vector3 (90.0f, 100.0f, 0.0f);
		speechBubble.transform.localScale = new Vector3 (1.0f,1.0f,1.0f);
		speechBubble.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		//Check for any available food to pick up
		if (GameHandler.instance.foodList.Count > 0) {
			currentFood = GameHandler.instance.foodList[0];
		}

		//if available
		if (currentFood != null) {
			transform.localPosition = Vector3.MoveTowards(this.transform.localPosition,currentFood.transform.localPosition,speed * Time.deltaTime);

			//When close enough
			if(Vector3.Distance(transform.localPosition,currentFood.transform.localPosition) < 100)
			{
				if(GameHandler.instance.foodList.Contains(currentFood))
				{
					GameHandler.instance.foodList.Remove(currentFood);
					Destroy(currentFood.gameObject);


					GameHandler.instance.healthBar.value += 2;
					GameHandler.instance.playerHealth = (int)GameHandler.instance.healthBar.value;
				}

			}
		}


	}

	//When a drop hits the ground unanswered
	public void showSpeechBubble()
	{
		speechBubble.SetActive (true);
		Invoke("hideSpeechBubble",1.5f);
	}
	void hideSpeechBubble()
	{
		speechBubble.SetActive (false);
	}

	//not used
	/*void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log ("OnTriggerEnter called");
		if (collider.tag == "food") {
			if(GameHandler.instance.foodList.Contains(collider.gameObject))
			{
				GameHandler.instance.foodList.Remove(collider.gameObject);
			}
			Destroy(collider.gameObject.GetComponent<BoxCollider2D>());
			Destroy(collider.gameObject);
		}
	}*/
}
