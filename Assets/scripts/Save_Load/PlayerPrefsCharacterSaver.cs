using UnityEngine;

public class PlayerPrefsCharacterSaver : MonoBehaviour
{
    public CharacterData characterData;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveCharacter(characterData, 0);

        if (Input.GetKeyDown(KeyCode.L))
            characterData = LoadCharacter(0);
    }

    static void SaveCharacter(CharacterData data, int characterSlot)
    {
        /*PlayerPrefs.SetString("characterName_CharacterSlot" + characterSlot, data.characterName);
        PlayerPrefs.SetFloat("power_CharacterSlot" + characterSlot, data.power);
        PlayerPrefs.SetInt("bullets_CharacterSlot" + characterSlot, data.bullets);*/
        PlayerPrefs.SetInt("bullets_CharacterSlot" + characterSlot, data.bullets);
        PlayerPrefs.Save();
    }

    static CharacterData LoadCharacter(int characterSlot)
    {
        CharacterData loadedCharacter = new CharacterData();
        /*loadedCharacter.characterName = PlayerPrefs.GetString("characterName_CharacterSlot" + characterSlot);
        loadedCharacter.power = PlayerPrefs.GetFloat("power_CharacterSlot" + characterSlot);
        loadedCharacter.bullets = PlayerPrefs.GetInt("bullets_CharacterSlot" + characterSlot);*/

        loadedCharacter.bullets = PlayerPrefs.GetInt("bullets_CharacterSlot" + characterSlot);

        return loadedCharacter;
    }
}