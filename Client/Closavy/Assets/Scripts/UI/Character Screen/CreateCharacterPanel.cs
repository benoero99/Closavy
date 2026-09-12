using System.Collections;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CreateCharacterPanel : MonoBehaviour
{
    [SerializeField] private CharacterScreenManager characterScreenManager;
    [SerializeField] private TMP_InputField newCharacterNameTIF;
    private const string BaseUrl = "http://localhost:5207";

    public void FinishCharacterCreationButtonPressed()
    {
        if (!ValidCharacterName(newCharacterNameTIF.text))
        {
            Debug.LogError("Invalid character name!");
            return;
        }

        StartCoroutine(CreateCharacter(newCharacterNameTIF.text));
    }

    public void CancelCharacterCreationButtonPressed()
    {
        characterScreenManager.SetCharacterSelectionPanelActive();
    }

    private IEnumerator CreateCharacter(string characterName)
    {
        Debug.Log("CreateCharacter called");

        CharacterRequest characterRequest = new()
        {
            Name = characterName,
        };

        string requestJson = JsonConvert.SerializeObject(characterRequest);

        Debug.Log("characterRequest: " + requestJson);

        using var request = UnityWebRequest.Post($"{BaseUrl}/Character", requestJson, "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Request failed: {request.error}");
            yield break;
        }

        var json = request.downloadHandler.text;
        var characterResponse = JsonConvert.DeserializeObject<CharacterResponse>(json);
        
        characterScreenManager.CharacterCreated(characterResponse);
    }

    private bool ValidCharacterName(string name)
    {
        return 3 <= name.Length && name.Length <= 20 && Regex.IsMatch(name, @"^\p{L}+(?: \p{L}+)*$");
    }
}
