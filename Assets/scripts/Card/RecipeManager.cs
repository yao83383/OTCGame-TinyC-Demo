using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;
public class RecipeManager : MonoBehaviour
{
    // 静态变量来保存Manager的实例  
    private static RecipeManager _instance;

    // 私有构造函数，防止外部通过new创建实例  
    private RecipeManager() { }

    // 公共静态方法，用于获取Manager的实例  
    public static RecipeManager Instance
    {
        get
        {
            // 如果_instance为空，则寻找场景中的Manager实例  
            if (_instance == null)
            {
                _instance = FindObjectOfType<RecipeManager>();

                // 如果没有找到，则创建一个新的Manager实例并添加到场景中  
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("RecipeManager");
                    _instance = managerObject.AddComponent<RecipeManager>();
                }
            }

            return _instance;
        }
    }

    // 初始化方法，可以在这里添加初始化代码  
    void Awake()
    {
        // 确保Manager是单例  
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        // 在这里添加初始化代码...  
    }

    public List<Recipe> Recipedata = new List<Recipe>();
    void Start()
    {
        // 假设JSON文件放在Resources文件夹下  
        //string jsonPath = Path.Combine(Application.streamingAssetsPath, "recipes.json");
        //string jsonData = File.ReadAllText(jsonPath);
        //recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonData); //JsonUtility.FromJson<List<Recipe>>(jsonData);
    }
}
