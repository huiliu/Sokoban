using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;
using System.Text;

public class EditorResourceMgr
        : IResourceMgr
    {
        public void Init()
        {
            this.LoadedResource = new Dictionary<string, Object>();
        }

        public void Fini()
        {
        }

        private readonly StringBuilder sbPath = new StringBuilder(256);
        private string GetResourceFullPath(string name, ResourceType type)
        {
            this.sbPath.Clear();
            this.sbPath.Append("Assets/");
            var rpath = ResourcePath.GetResourceFullPath(name, type);
            this.sbPath.Append(rpath);
            return this.sbPath.ToString();
        }

        private Dictionary<string, Object> LoadedResource;
        public void LoadPrefab(string name, Action<GameObject> cb)
        {
            var go = null as GameObject;
            do
            {
                var path = this.GetResourceFullPath(name, ResourceType.Prefab);
                var obj = null as Object;
                if (this.LoadedResource.TryGetValue(path, out obj))
                {
                    go = GameObject.Instantiate(obj as GameObject);
                    break;
                }

                go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (go == null)
                {
                    break;
                }

                this.LoadedResource.Add(path, go);
                go = GameObject.Instantiate(go);
            } while (false);

            cb.SafeInvoke(go);
        }

        public void LoadTexture(string name, Action<Sprite> cb)
        {
            var go = null as Sprite;
            do
            {
                var path = this.GetResourceFullPath(name, ResourceType.Texture);
                var obj = null as Object;
                if (this.LoadedResource.TryGetValue(path, out obj))
                {
                    go = obj as Sprite;
                    break;
                }

                go = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                if (go == null)
                {
                    break;
                }

                this.LoadedResource.Add(path, go);
            } while (false);

            cb.SafeInvoke(go);
        }

        private T GetFromCache<T>(string key) where T : Object
        {
            var obj = null as Object;
            this.LoadedResource.TryGetValue(key, out obj);

            return obj as T;
        }

        public void LoadTextAsset(string name, Action<string> cb)
        {
            var textAsset = null as TextAsset;
            do
            {
                var path = this.GetResourceFullPath(name, ResourceType.TextAsset);
                var obj = null as Object;
                if (this.LoadedResource.TryGetValue(path, out obj))
                {
                    textAsset = obj as TextAsset;
                    break;
                }

                textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                if (textAsset == null)
                    break;

                this.LoadedResource.Add(path, textAsset);
            } while (false);

            cb.SafeInvoke(textAsset?.text);
        }
    }