using System;
using System.Text;

    public enum ResourceType
    {
        Audio,
        Animation,
        Scene,
        Prefab,
        Texture,
        TextAsset,
    }

public static class ResourcePath
{
    public const string kAssetBundlesPath = "AssetBundles/";
    public const string kAnimationPath = "Animations/";
    public const string kAudioPath = "Audio/";
    public const string kScenePath = "Scenes/";
    public const string kPrefabPath = "Prefabs/";
    public const string kTexturePath = "Textures/";
    public const string kTextAssetPath = "TextAssets/";

    private static StringBuilder sb = new StringBuilder(256);
    public static string GetResourceFullPath(string name, ResourceType type)
    {
        sb.Clear();
        sb.Append("Assets/");

        switch(type)
        {
            case ResourceType.Animation:
                sb.Append(kAnimationPath);
                break;
            case ResourceType.Audio:
                sb.Append(kAudioPath);
                break;
            case ResourceType.Prefab:
                sb.Append(kPrefabPath);
                break;
            case ResourceType.Scene:
                sb.Append(kScenePath);
                break;
            case ResourceType.Texture:
                sb.Append(kTexturePath);
                break;
            case ResourceType.TextAsset:
                sb.Append(kTextAssetPath);
                break;
            default:
                Base.Log.Assert(false, "不存在的资源类型：{0}", type);
                return null;
        }

        sb.Append(name);

        return sb.ToString();
    }


        /// <summary>
        /// 获取Prefab在Assets目录下的全路径
        ///
        /// 如果是在kPrefabPath的子目录中，名称中需要包含其路径。如：
        /// prefab路径为：Prefab/Characters/hero01.prefab
        /// 则name应该为: Characters/hero01.prefab
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetPrefabFileFullPath(string name)
        {
            sb.Clear();
            sb.Append(kPrefabPath);
            sb.Append(name);

            return sb.ToString();
        }

        private static string GetTextureFileFullPath(string name)
        {
            sb.Clear();
            sb.Append(kTexturePath);
            sb.Append(name);

            return sb.ToString();
        }

        private static string GetSceneFileFullPath(string name)
        {
            sb.Clear();
            sb.Append(kScenePath);
            sb.Append(name);

            return sb.ToString();
        }

        private static string GetAnimationsFileFullPath(string name)
        {
            sb.Clear();
            sb.Append(kAnimationPath);
            sb.Append(name);

            return sb.ToString();
        }

        private static string GetAudioFileFullPath(string name)
        {
            sb.Clear();
            sb.Append(kAudioPath);
            sb.Append(name);

            return sb.ToString();
        }
    }
