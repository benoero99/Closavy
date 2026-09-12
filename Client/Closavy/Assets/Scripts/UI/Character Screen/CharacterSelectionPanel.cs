using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CharacterSelectionPanel : MonoBehaviour
{
    [SerializeField] private CharacterScreenManager characterScreenManager;
    [SerializeField] private TMP_Text displayNameText;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text characterLevelText;
    [SerializeField] private TMP_Text characterExperienceText;

    private List<CharacterResponse> characters;
    private int currentCharacterIndex;

    private const string BaseUrl = "http://localhost:5207";

    void Start()
    {
        StartCoroutine(GetDisplayName());
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame)
        {
            SelectPreviousCharacter();
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            SelectNextCharacter();
        }
    }

    public void AddCharacterButtonPressed()
    {
        characterScreenManager.SetAddCharacterPanelActive();
    }

    public void LogoutButtonPressed()
    {
        StartCoroutine(Logout());
    }

    public void AddCharacter(CharacterResponse character)
    {
        characters.Add(character);
        currentCharacterIndex = characters.FindIndex(c => c.Id == character.Id);

        UpdateCharacterDisplay();
    }

    private IEnumerator GetDisplayName()
    {
        Debug.Log("GetDisplayName called");

        using var request = UnityWebRequest.Get($"{BaseUrl}/Account/LoggedInAccount");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Request failed: {request.error}");
            yield break;
        }

        var json = request.downloadHandler.text;
        Debug.Log("Current logged in account: " + json);

        var accountRespone = JsonConvert.DeserializeObject<AccountRespone>(json);
        displayNameText.text = accountRespone.DisplayName;

        StartCoroutine(GetCharacters());
    }

    private IEnumerator GetCharacters()
    {
        Debug.Log("GetCharacters called");

        using var request = UnityWebRequest.Get($"{BaseUrl}/Character/Characters");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Request failed: {request.error}");
            yield break;
        }

        var json = request.downloadHandler.text;
        Debug.Log("Current users characters: " + json);

        List<CharacterResponse> charactersResponse = JsonConvert.DeserializeObject<List<CharacterResponse>>(json);
        characters = charactersResponse;

        if (characters.Count != 0)
        {
            currentCharacterIndex = 0;
            UpdateCharacterDisplay();
        }
    }

    private void SelectPreviousCharacter()
    {
        if (characters.Count == 0)
            return;

        currentCharacterIndex--;

        if (currentCharacterIndex < 0)
        {
            currentCharacterIndex = characters.Count - 1;
        }

        UpdateCharacterDisplay();
    }

    private void SelectNextCharacter()
    {
        if (characters.Count == 0)
            return;

        currentCharacterIndex++;

        if (currentCharacterIndex > characters.Count - 1)
        {
            currentCharacterIndex = 0;
        }

        UpdateCharacterDisplay();
    }

    private void UpdateCharacterDisplay()
    {
        characterNameText.text = characters[currentCharacterIndex].Name;
        characterLevelText.text = $"Level: {characters[currentCharacterIndex].Level}";
        characterExperienceText.text = $"XP: {characters[currentCharacterIndex].Experience}";
    }

    private IEnumerator Logout()
    {
        Debug.Log("Logout pressed");

        using var request = UnityWebRequest.PostWwwForm($"{BaseUrl}/Auth/Logout", "");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Request failed: {request.error}");
            yield break;
        }

        Debug.Log("Logout successful");

        SceneManager.LoadScene(0);
    }
}
