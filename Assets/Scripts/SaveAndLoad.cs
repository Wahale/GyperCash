using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private string _path;
    private string data = "data.json";
    public Save save = new Save();

    private void Awake()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath,data);
        #else
            _path = Path.Combine(Application.dataPath, data);
        #endif

        LoadFromFile();
    }

    public void SaveToFile()
    {
        File.WriteAllText(_path, JsonUtility.ToJson(save));
    }

    private void LoadFromFile()
    {
        if (File.Exists(_path))
        {
            save = JsonUtility.FromJson<Save>(File.ReadAllText(_path));
        }
    }

    private void OnApplicationQuit()
    {
        SaveToFile();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (Application.platform == RuntimePlatform.Android)
            SaveToFile();
    }
}

[System.Serializable]
public class Save
{

}