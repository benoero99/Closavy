using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

        yield return ApiClient.Get<AccountRespone>("Account/LoggedInAccount", result =>
        {
            if (!result.Success)
            {
                Debug.LogError($"Request failed: {result.ProblemDetails.Detail}");
                return;
            }

            displayNameText.text = result.Data.DisplayName;

            StartCoroutine(GetCharacters());
        });
    }

    private IEnumerator GetCharacters()
    {
        Debug.Log("GetCharacters called");

        yield return ApiClient.Get<List<CharacterResponse>>("Character/Characters", result =>
        {
            if (!result.Success)
            {
                Debug.LogError($"Request failed: {result.ProblemDetails.Detail}");
                return;
            }

            characters = result.Data;

            if (characters.Count != 0)
            {
                currentCharacterIndex = 0;
                UpdateCharacterDisplay();
            }
        });
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

        yield return ApiClient.Post<string, string>("Auth/Logout", "", result =>
        {
            if (!result.Success)
            {
                Debug.LogError($"Request failed: {result.ProblemDetails.Detail}");
                return;
            }

            Debug.Log("Logout successful");

            SceneManager.LoadScene(0);
        });
    }
}
