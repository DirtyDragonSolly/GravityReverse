using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text highScore;
	Text _nowScore;

	float _score = 0;

	private void Start()
	{
		_nowScore = GetComponent<Text>();
		_nowScore.text = _score.ToString();
	}

	private void FixedUpdate()
	{
		_score += 1 * Time.fixedDeltaTime;
		_nowScore.text = _score.ToString("0");

		var floatScore = float.Parse(highScore.text);

		if (floatScore < _score)
		{
			PlayerPrefs.SetString("HighScore", _score.ToString("0"));
		}
	}

	public void Respawn()
	{
		_score = 0;
	}
}
