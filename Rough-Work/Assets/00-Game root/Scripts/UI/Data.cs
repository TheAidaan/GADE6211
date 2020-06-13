using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Data : MonoBehaviour
{
	const string privateCode = "SmO9YxIkLEWUgjkXFv5KqABrWksJlPHEmstjvNl4Xoxw";
	const string publicCode = "5ee36217377e860b6c46a40a";
	const string webURL = "http://dreamlo.com/lb/";

	public Highscore[] highscoresList;
	static Data instance;

	void Awake()
	{
		instance = this;
	}

	public static void AddNewHighscore(string username, int score)
	{
		instance.StartCoroutine(instance.UploadNewHighscore(username, score));
	}

	IEnumerator UploadNewHighscore(string username, int score)
	{
		UnityWebRequest www = UnityWebRequest.Get(webURL + privateCode + "/add/"+ username + "/" + score);
		yield return www.SendWebRequest(); 

		if (www.isNetworkError || www.isHttpError)
		{
			print("Error Downloading: " + www.error);
		}
	}

	public void DownloadHighscores()
	{
		StartCoroutine(DownloadHighscoresFromDatabase());
	}

	IEnumerator DownloadHighscoresFromDatabase()
	{
		UnityWebRequest www = UnityWebRequest.Get(webURL + publicCode + "/pipe/");
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			print("Error Downloading: " + www.error);

		}else
		{
			FormatHighscores(www.downloadHandler.text);
			FindObjectOfType<Leaderboard>().OnHighscoresDownloaded(highscoresList);
		}
		
	}

	void FormatHighscores(string textStream)
	{
		string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];

		for (int i = 0; i < entries.Length; i++)
		{
			string[] entryInfo = entries[i].Split(new char[] { '|' });
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			highscoresList[i] = new Highscore(username, score);
		}
	}
}

public struct Highscore
{
	public string username;
	public int score;

	public Highscore(string _username, int _score)
	{
		username = _username;
		score = _score;
	}

}