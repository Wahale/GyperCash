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
        
            _path = Path.Combine(Application.persistentDataPath, data);
        #else
            _path = Path.Combine(Application.dataPath, data);
        #endif

        LoadFromFile();
    }

    public void SaveToFile()
    {
        save._hp = PlayerController.hp;
        save._speedPlayer = PlayerController.speedPlayer;
        save._rangeLantern = PlayerController.rangeLantern;
        save._spotAngle = PlayerController.spotAngle;

        File.WriteAllText(_path, JsonUtility.ToJson(save));
    }

    private void LoadFromFile()
    {
        if (File.Exists(_path))
        {
            save = JsonUtility.FromJson<Save>(File.ReadAllText(_path));

            PlayerController.hp = save._hp;
            PlayerController.speedPlayer = save._speedPlayer;
            PlayerController.rangeLantern = save._rangeLantern;
            PlayerController.spotAngle = save._spotAngle;
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

public class Save
{
    public int _hp;
    public float _speedPlayer;
    public float _rangeLantern;
    public float _spotAngle;
}