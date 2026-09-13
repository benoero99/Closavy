using UnityEngine;

public class CharacterScreenManager : MonoBehaviour
{
    [SerializeField] private CharacterSelectionPanel characterSelectionPanel;
    [SerializeField] private CreateCharacterPanel createCharacterPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetCharacterSelectionPanelActive();
    }

    public void SetCharacterSelectionPanelActive()
    {
        characterSelectionPanel.gameObject.SetActive(true);
        createCharacterPanel.gameObject.SetActive(false);
    }

    public void SetAddCharacterPanelActive()
    {
        characterSelectionPanel.gameObject.SetActive(false);
        createCharacterPanel.gameObject.SetActive(true);
    }

    public void CharacterCreated(CharacterResponse character)
    {
        characterSelectionPanel.AddCharacter(character);
        SetCharacterSelectionPanelActive();
    }
}
