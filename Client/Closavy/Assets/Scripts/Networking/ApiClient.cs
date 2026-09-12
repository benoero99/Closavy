using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ApiClient : MonoBehaviour
{
    private const string BaseUrl = "http://localhost:5207";

    private void Start()
    {
        StartCoroutine(TestConnection());
    }

    private IEnumerator TestConnection()
    {
        using var request = UnityWebRequest.Get($"{BaseUrl}/Character/1");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var json = request.downloadHandler.text;

            var character = JsonUtility.FromJson<CharacterResponse>(json);

            Debug.Log($"Character: {character.Name}");
            Debug.Log($"Level: {character.Level}");
        }
        else
        {
            Debug.LogError($"Request failed: {request.error}");
        }
    }
}
