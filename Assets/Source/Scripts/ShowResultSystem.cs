using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ShowResultSystem: MonoBehaviour
{
    [SerializeField] TextMeshProUGUI result;
    [SerializeField] TextMeshProUGUI record;
    [SerializeField] TextMeshProUGUI leaderboardText;

    private Dictionary<string, float> playerLeaderboard = new Dictionary<string, float>();
    private string jsonURI = "https://taptics.b-cdn.net/files/leaderboard.json";
    private string loadedText;
    private float localRecord, lastResult;

    private void OnEnable()
    {
        localRecord = PlayerPrefs.GetFloat("lastResult");
        lastResult = PlayerPrefs.GetFloat("localRecord");

        result.text = "Result: " + localRecord;
        record.text = "Record: " + lastResult;

        StartCoroutine(CreateLeaderboard(jsonURI));
    }

    IEnumerator CreateLeaderboard(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            loadedText = webRequest.downloadHandler.text;
            Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            ScoreList newList = JsonUtility.FromJson<ScoreList>("{\"players\":" + loadedText + "}");

            for (int i = 0; i < newList.players.Length; i++)
            {
                playerLeaderboard.Add(newList.players[i].name, newList.players[i].score);
            }

            playerLeaderboard.Add("YOU", localRecord);

            if (localRecord != lastResult)
            {
                playerLeaderboard.Add("ALSO YOU", lastResult);
            }
            var sortedDictionary = playerLeaderboard.OrderByDescending(player => player.Value);

            foreach (var playerScore in sortedDictionary)
            {
                leaderboardText.text += string.Format("{0} , {1}\n", playerScore.Key, playerScore.Value);
            }
        }
    }
}
