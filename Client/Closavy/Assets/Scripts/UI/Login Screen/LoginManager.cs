using System.Collections;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScreen : MonoBehaviour
{
    [SerializeField] private TMP_InputField displayNameText;
    private const string BaseUrl = "http://localhost:5207";

    public void LoginPressed()
    {
        StartCoroutine(Login(displayNameText.text));
    }

    private IEnumerator Login(string displayName)
    {
        Debug.Log("Display name: " + displayNameText.text);

        LoginRequest loginRequest = new()
        {
            DisplayName = displayName,
        };

        string json = JsonConvert.SerializeObject(loginRequest);

        Debug.Log("Login request json: " + json);

        using var request = UnityWebRequest.Post($"{BaseUrl}/Auth/Login", json, "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Request failed: {request.error}");
            yield break;
        }

        Debug.Log("Login successful");

        SceneManager.LoadScene(1);
    }
}
