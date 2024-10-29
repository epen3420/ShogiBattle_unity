using UnityEngine;
using UnityEngine.AddressableAssets;
using System.IO;


public class JsonManager
{
    /// <summary>
    /// 欲しいJsonの型TとJsonのfilePathを渡すことでJsonファイルからデータを取得する関数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static T LoadFromAddressables<T>(string filePath)
    {
        string json = Addressables.LoadAssetAsync<TextAsset>(filePath).WaitForCompletion().ToString(); // ファイルからJSON読み込み
        Debug.Log($"Loaded the json file");
        return JsonUtility.FromJson<T>(json); // JSONをオブジェクトに変換
    }

    public static T LoadFromLocal<T>(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath); // ファイルからJSON読み込み
                return JsonUtility.FromJson<T>(json); // JSONをオブジェクトに変換
            }
            else
            {
                Debug.LogWarning($"File not found: {filePath}");
                return default;
            }
        }
        catch (IOException ex)
        {
            Debug.LogError($"Failed to load JSON: {ex.Message}");
            return default;
        }
    }

    public static void Save<T>(T data, string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        StreamWriter writer;

        string json = JsonUtility.ToJson(data, true); // データをJSONに変換

        writer = new StreamWriter(filePath, false);
        writer.Write(json);
        writer.Flush();
        writer.Close();

        Debug.Log($"JSON saved to {filePath}");
    }
}
