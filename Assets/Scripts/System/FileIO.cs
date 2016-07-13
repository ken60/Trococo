using UnityEngine;
using System.IO;

public class FileIO : MonoBehaviour
{
    public static void FileWrite(string name, string json)
    {
        // 保存するフォルダー
        string path = Application.persistentDataPath + "/Database/";

        // フォルダーがない場合は作成する
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + name + ".json", json);
    }

    public static string FileRead(string name)
    {
        string path = Application.persistentDataPath + "/Database/";

        //ファイルがない場合は作成
        if (!Directory.Exists(path))
        {
            FileWrite("SaveData", "");
        }
        return File.ReadAllText(path + name + ".json", System.Text.Encoding.UTF8);
    }

    public static void Delete(string path)
    {
        if (!Directory.Exists(path))
        {
            return;
        }

        //ディレクトリ以外の全ファイルを削除
        string[] filePaths = Directory.GetFiles(path);
        foreach (string filePath in filePaths)
        {
            File.SetAttributes(filePath, FileAttributes.Normal);
            File.Delete(filePath);
        }

        //ディレクトリの中のディレクトリも再帰的に削除
        string[] directoryPaths = Directory.GetDirectories(path);
        foreach (string directoryPath in directoryPaths)
        {
            Delete(directoryPath);
        }

        //中が空になったらディレクトリ自身も削除
        Directory.Delete(path, false);
    }
}