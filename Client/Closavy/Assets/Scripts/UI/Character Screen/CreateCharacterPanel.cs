using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class CreateCharacterPanel : MonoBehaviour
{
    [SerializeField] private CharacterScreenManager characterScreenManager;
    [SerializeField] private TMP_InputField newCharacterNameTIF;
    [SerializeField] private TMP_Text errorMessageText;

    void OnEnable()
    {
        newCharacterNameTIF.text = string.Empty;
    }

    public void EmptyErrorMessage(string value)
    {
        errorMessageText.text = string.Empty;
    }

    public void FinishCharacterCreationButtonPressed()
    {
        if (!ValidCharacterName(newCharacterNameTIF.text))
        {
            errorMessageText.text = "Invalid character name!";
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

        yield return ApiClient.Post<CharacterRequest, CharacterResponse>("Character", characterRequest, result =>
        {
            if (!result.Success)
            {
                errorMessageText.text = result.ProblemDetails.Title;
                return;
            }

            characterScreenManager.CharacterCreated(result.Data);
        });
    }

    private bool ValidCharacterName(string name)
    {
        return 3 <= name.Length && name.Length <= 20 && Regex.IsMatch(name, @"^\p{L}+(?: \p{L}+)*$");
    }
}
