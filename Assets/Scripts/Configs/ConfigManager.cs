using System.IO;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    private static ConfigManager instance;

    private static ConfigData _configData;

    private string _configFilePath;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        _configFilePath = Application.persistentDataPath + "/config.json";

        LoadConfig();
    }

    public void SaveConfig()
    {
        string jsonData = JsonUtility.ToJson(_configData);
        File.WriteAllText(_configFilePath, jsonData);
    }

    public void LoadConfig()
    {
        if (File.Exists(_configFilePath))
        {
            string jsonData = File.ReadAllText(_configFilePath);
            _configData = JsonUtility.FromJson<ConfigData>(jsonData);
        }
        else
        {

            _configData = new ConfigData(0);
            SaveConfig();
        }
    }

    public static void SetInteractRadius(float radius)
    {
        _configData.SetInteractRadius(radius);
        instance.SaveConfig();
    }

    public static float GetInteractRadius()
    {
        return _configData.GetInteractRadius();
    }

}
