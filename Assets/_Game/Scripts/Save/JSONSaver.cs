using UnityEngine;
using System.IO;
public static class JSONSaver
{
    private static string SAVE = "SAVE";

	public static bool FileExist()
	{
		return PlayerPrefs.HasKey(SAVE);
	}

    public static SaveData Load(SaveData defaultData)
    {
        string defaultText = JsonUtility.ToJson(defaultData);
        string loadedString = PlayerPrefs.GetString(SAVE, defaultText);
        Debug.Log("LOAD:" + loadedString);
        return JsonUtility.FromJson<SaveData>(loadedString);
    }

	public static void Save(SaveData data)
	{
        string text = JsonUtility.ToJson(data);
        Debug.Log("SAVE:" + text);
        PlayerPrefs.SetString(SAVE, text);
        
	}


	public static void Delete()
	{
        PlayerPrefs.DeleteKey(SAVE);
	}
    

}
