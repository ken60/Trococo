using UnityEngine;
using System.IO;

public class FileIO : MonoBehaviour
{
    //ファイル書き込み
    public static void FileWrite(string name, string json)
    {
        // 保存するフォルダー
        string path = Application.persistentDataPath + "/Database/";

        //ディレクトリがない場合は作成する
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //ファイルがない場合は作成
        if (!File.Exists(path + name + ".json"))
        {
            CreateFile(path + name + ".json");
        }

        File.WriteAllText(path + name + ".json", json);
    }

    //ファイル読み込み
    public static string FileRead(string name)
    {
        string path = Application.persistentDataPath + "/Database/";

        //ディレクトリがない場合は作成
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //ファイルがない場合は作成
        if (!File.Exists(path + name + ".json"))
        {
            CreateFile(path + name + ".json");
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

    //ファイル作成
    private static void CreateFile(string path)
    {
        using (FileStream fs = File.Create(path))
        {
            // ファイルストリームを閉じて、変更を確定させる
            // 呼ばなくても using を抜けた時点で Dispose メソッドが呼び出される
            fs.Close();
            print("CreateFile " + path);
        }
    }
}