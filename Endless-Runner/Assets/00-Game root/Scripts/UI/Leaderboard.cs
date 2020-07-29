using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
	TextMeshProUGUI[] leaderBoardTexts = new TextMeshProUGUI[5];
	Data data;

	void Start()
	{
		data = GetComponentInParent<Data>();
		for (int i = 0; i < leaderBoardTexts.Length; i++)
		{
			leaderBoardTexts[i] = transform.GetChild(i).gameObject.GetComponent< TextMeshProUGUI >();
		}

		for (int i = 0; i < leaderBoardTexts.Length; i++)
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
				leaderBoardTexts[i].text += highscoreList[i].username + '\t' + " - "  +'\t' + highscoreList[i].score;
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
