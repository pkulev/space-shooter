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

    public readonly string id;
    public readonly string name;
    public readonly int score;

    [JsonConstructor]
    public ScoreData(string id, string name, int score) {
        this.id = id;
        this.name = name;
        this.score = score;
    }

    public ScoreData(Guid id, string name, int score) : this(id.ToString(), name, score) {}
    public ScoreData(string name, int score) : this(Guid.NewGuid().ToString(), name, score) {}

    //[JsonConstructor]
    public ScoreData(string id, string name, string score) : this(id, name, Convert.ToInt32(score)) {}

    public Dictionary<string, object> ToJwtDict() {
        return new Dictionary<string, object> {
            { "_id", id },
            { "name", name },
            { "score", score },
        };
    }

    public string ToSlotItem() {
        return string.Format("{0}: {1}", name, score);
    }
}


/// <summary>
/// Simple client for Highscores server.
/// </summary>
static class HighScoresClient {
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

    public static async Task<bool> Add(ScoreData score) {
        var uri = new Uri(_base_url, "add");

        IJwtEncoder encoder = new JwtEncoder(
            new HMACSHA256Algorithm(),
            new JsonNetSerializer(),
            new JwtBase64UrlEncoder());

        var token = encoder.Encode(score.ToJwtDict(), _secret);

        var request = new HttpRequestMessage() {
            RequestUri = uri,
            Method = HttpMethod.Get,
        };

        request.Headers.Add("Token", token);

        try {
            var resp = await _client.SendAsync(request);
            var body = await resp.Content.ReadAsStringAsync();
            return resp.IsSuccessStatusCode && body != "Not authorized.\n";
        } catch (Exception exc) {
            Debug.LogError("Error while sending new score: " + exc);
            return false;
        }
    }
}

public class HighScores : MonoBehaviour
{
    public GameObject[] scoreSlots;
    public GameObject notificationArea;

    private async void Start() {
     
        if (await Ping()) {
            UpdateScores();
        }
    }

    public async Task<bool> Ping() {
        notificationArea.SetActive(false);

        bool status = await HighScoresClient.Ping();
        UpdateStatusText(status);
        return status;
    }

    private static Text GetScoreSlotText(GameObject slot) {
        return slot.GetComponentInChildren<Text>();
    }

    private void UpdateStatusText(bool successful) {
        if (!successful) {
            notificationArea.SetActive(true);
        }
    }

    private void UpdateScores() {
        var scores = HighScoresClient.Top(scoreSlots.Length);
        var idx = 0;
        foreach (var score in scores) {
            GetScoreSlotText(scoreSlots[idx]).text = score.ToSlotItem();
            idx++;
        }
    }

    private void Update() {
    }
}