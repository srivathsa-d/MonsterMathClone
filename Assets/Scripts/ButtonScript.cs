using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	public Button button;
	public Text text;
	bool pressed;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		text = GetComponentInChildren<Text> ();
		pressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!pressed)
		{
			int num = int.Parse(text.text);
			button.onClick.AddListener(() => OnButtonPress(num));
			pressed = true;
		}
	}
	void OnButtonPress(int num)
	{
		//Allow upto 2 digit input. Else reset input
		if (GameHandler.instance.inputValue == 0) {
			GameHandler.instance.inputValue = num;
		} else {
			GameHandler.instance.inputValue *= 10;
			GameHandler.instance.inputValue += num;
		}

		//Update gui
		GameHandler.instance.inputText.GetComponent<Text> ().text = GameHandler.instance.inputValue.ToString();

		//Get current expected value
		int correctAnswer = GameHandler.instance.getCurrentDrop ();

		//If player entered correct value, change the sprite image and increase difficulty. 
		if (correctAnswer == GameHandler.instance.inputValue) {

			GameHandler.instance.dropList[0].GetComponent<Drops>().sprite.sprite = Resources.Load<Sprite>("Sprites/food");
			GameHandler.instance.dropList[0].GetComponent<Drops>().isAnswered = true;
			GameHandler.instance.dropList [0].GetComponent<Drops> ().destroyThis (false);
			GameHandler.instance.correctAnswerCount++;
			if(GameHandler.instance.correctAnswerCount % 2 == 0 )
			{
				Debug.Log("increasing range");
				if(GameHandler.instance.min<10)
				{
					GameHandler.instance.min++;
					GameHandler.instance.range++;
				}
			}

			Invoke("resetInputValue",0.5f);

		} else if(GameHandler.instance.inputValue > 99) {
			GameHandler.instance.inputValue = 0;
			GameHandler.instance.inputText.GetComponent<Text> ().text = "";
		}
	}

	void resetInputValue()
	{
		GameHandler.instance.inputText.GetComponent<Text> ().text = "";
		GameHandler.instance.inputValue = 0;
	}
}
