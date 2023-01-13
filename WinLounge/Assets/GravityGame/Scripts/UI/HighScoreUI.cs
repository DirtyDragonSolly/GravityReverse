using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    Text _score;

	private void Start()
	{
		_score= GetComponent<Text>();
	}

	private void Update()
	{
		_score.text = PlayerPrefs.GetString("HighScore", "0");
	}
}
