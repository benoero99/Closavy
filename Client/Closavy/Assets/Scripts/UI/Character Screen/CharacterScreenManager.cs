using UnityEngine;

public class CharacterScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject characterSelectionPanelGO;
    [SerializeField] private GameObject createCharacterPanelGO;
    [SerializeField] private CharacterSelectionPanel characterSelectionPanel;
    [SerializeField] private CreateCharacterPanel createCharacterPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetCharacterSelectionPanelActive();
    }

    public void SetCharacterSelectionPanelActive()
    {
        characterSelectionPanelGO.SetActive(true);
        createCharacterPanelGO.SetActive(false);
    }

    public void SetAddCharacterPanelActive()
    {
        characterSelectionPanelGO.SetActive(false);
        createCharacterPanelGO.SetActive(true);
    }

    public void CharacterCreated(CharacterResponse character)
    {
        characterSelectionPanel.AddCharacter(character);
        SetCharacterSelectionPanelActive();
    }
}
