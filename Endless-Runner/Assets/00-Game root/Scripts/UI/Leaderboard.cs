using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
	public TextMeshProUGUI[] leaderBoardTexts;
	Data data;

	void Start()
	{
		leaderBoardTexts = GetComponentsInChildren<TextMeshProUGUI>();
		data = GetComponent<Data>();

		for (int i = 1; i < leaderBoardTexts.Length; i++)
		{
			leaderBoardTexts[i].text = i + 1 + ". Fetching...";
		}

		StartCoroutine("RefreshHighscores");
	}

	public void OnHighscoresDownloaded(Highscore[] highscoreList)
	{
		for (int i = 0; i < leaderBoardTexts.Length; i++)
		{
			leaderBoardTexts[i].text = i + 1 + ". ";
			if (i < highscoreList.Length)
			{
				leaderBoardTexts[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
			}
		}
	}

	IEnumerator RefreshHighscores()
	{
		while (true)
		{
			data.DownloadHighscores();
			yield return new WaitForSeconds(30);
		}
	}
}
