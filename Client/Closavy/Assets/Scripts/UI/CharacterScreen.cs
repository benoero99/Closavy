using System.Collections;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text characterLevelText;
    [SerializeField] private TMP_Text characterExperienceText;

    private const string BaseUrl = "http://localhost:5207";

    public void DisplayCharacter(CharacterResponse character)
    {
        characterNameText.text = $"Name: {character.Name}";
        characterLevelText.text = $"Level: {character.Level}";
        characterExperienceText.text = $"XP: {character.Experience}";
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadCharacter());
    }

    private IEnumerator LoadCharacter()
    {
        using var request = UnityWebRequest.Get($"{BaseUrl}/Character/7");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Request failed: {request.error}");
            yield break;
        }

        var json = request.downloadHandler.text;
        Debug.Log($"Returned json: {json}");

        var character = JsonConvert.DeserializeObject<CharacterResponse>(json);

        Debug.Log($"Character name: {character.Name}");

        DisplayCharacter(character);
    }
}
