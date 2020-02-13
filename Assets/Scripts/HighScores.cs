using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


class ScoreData {

    private readonly string id;
    private readonly string name;
    private readonly int score;

    public ScoreData(string id, string name, int score) {
        this.id = id;
        this.name = name;
        this.score = score;
    }

    ScoreData(Dictionary<string, string> data) {
        this.id = data["_id"];
        this.name = data["name"];
        this.score = Convert.ToInt32(data["score"]);
    }

    public string ToSlotItem() {
        return string.Format("{0}: {1}", name, score);
    }
}


static class HighScoresServer {
    private static readonly string _secret = "keeek";
    private static readonly Uri _base_url = new Uri("http://ec2-52-91-188-222.compute-1.amazonaws.com:8000");
    private static readonly HttpClient _client = new HttpClient() {
        Timeout = TimeSpan.FromSeconds(10),
    };

    public static async Task<bool> Ping() {
        var uri = new Uri(_base_url, "ping");
        HttpResponseMessage resp = await _client.GetAsync(uri);
        string body = await resp.Content.ReadAsStringAsync();

        return resp.IsSuccessStatusCode && body == "Pong.\n";
    }

    public static List<ScoreData> Top(int amount = 10) {
        var uri = new Uri(_base_url, string.Format("/top?q={0}", amount));
        HttpResponseMessage resp = _client.GetAsync(uri).Result;
        var body = resp.Content.ReadAsStringAsync().Result;

        if (resp.IsSuccessStatusCode) {
            return JsonConvert.DeserializeObject<List<ScoreData>>(body);
        } else {
            return null;
        }
    }

    public static bool Add(ScoreData score) {
        var uri = new Uri(_base_url, "add");
        return true;
    }
}

public class HighScores : MonoBehaviour
{
    public Text serverStatusText;
    public GameObject[] scoreSlots;

    public async void Ping() {
        serverStatusText.text = "Connecting...";
        UpdateStatusText(await HighScoresServer.Ping());
    }

    private static Text GetScoreSlotText(GameObject slot) {
        return slot.GetComponentInChildren<Text>();
    }

    private void UpdateStatusText(bool successful) {
        if (successful) {
            serverStatusText.text = "OK";
        } else {
            serverStatusText.text = "Unavailable";
        }
    }

    private void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Get nudes")) {
            GetScores();
        }

        if (GUI.Button(new Rect(10, 130, 150, 100), "Send nudes")) {
            PostScores();
        }
    }

    private async void PostScores() {
        IJwtEncoder encoder = new JwtEncoder(
            new HMACSHA256Algorithm(),
            new JsonNetSerializer(),
            new JwtBase64UrlEncoder());

        var payload = new Dictionary<string, object> {
            { "_id", "3" },
            { "name", "Evgsol" },
            { "score", 123 },
        };

        var tmp = new Dictionary<string, Dictionary<string, object>> {
            { "Score", payload },
        };

        //var token = encoder.Encode(tmp, _secret);

        var request = new HttpRequestMessage() {
        //    RequestUri = new Uri(_highscores_service_production_url + "/add"),
            Method = HttpMethod.Get,
        };

        //request.Headers.Add("Authorization", string.Format("JWT Token={0}", token));
        //request.Headers.Add("Token", token);

        //var resp = await _client.SendAsync(request);
        //var body = await resp.Content.ReadAsStringAsync();

    }

    private void GetScores() {
        var scores = HighScoresServer.Top();
        var idx = 0;
        foreach (var score in scores) {
            GetScoreSlotText(scoreSlots[idx]).text = score.ToSlotItem();
            if (++idx >= scores.Count) {
                break;
            }
        }
    }

    private void Update() {
    }
}