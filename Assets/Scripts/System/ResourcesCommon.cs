using UnityEngine;
using UnityEngine.UI;

public class ResourcesCommon : MonoBehaviour
{
    public static T Load<T>(string path) where T : Object
    {
        return Resources.Load(path, typeof(T)) as T;
    }

    public static Texture LoadTexture(string name)
    {
        return Load<Texture>("Texture/" + name);
    }

    public static GameObject LoadPrefab(string name)
    {
        return Load<GameObject>("GameObject/" + name);
    }

    public static Sprite LoadSprite(string name)
    {
        return Load<Sprite>("Sprite/" + name);
    }

    public static Image LoadImage(string name)
    {
        return Load<Image>("Image/" + name);
    }

    public static AudioClip LoadAudioClip(string name)
    {
        return Load<AudioClip>("AudioClip/" + name);
    }


    public static void ResourcesUnload()
    {
        Resources.UnloadUnusedAssets();
    }

}