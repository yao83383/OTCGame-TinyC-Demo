using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;
public class RecipeManager : MonoBehaviour
{
    // ��̬����������Manager��ʵ��  
    private static RecipeManager _instance;

    // ˽�й��캯������ֹ�ⲿͨ��new����ʵ��  
    private RecipeManager() { }

    // ������̬���������ڻ�ȡManager��ʵ��  
    public static RecipeManager Instance
    {
        get
        {
            // ���_instanceΪ�գ���Ѱ�ҳ����е�Managerʵ��  
            if (_instance == null)
            {
                _instance = FindObjectOfType<RecipeManager>();

                // ���û���ҵ����򴴽�һ���µ�Managerʵ������ӵ�������  
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("RecipeManager");
                    _instance = managerObject.AddComponent<RecipeManager>();
                }
            }

            return _instance;
        }
    }

    // ��ʼ��������������������ӳ�ʼ������  
    void Awake()
    {
        // ȷ��Manager�ǵ���  
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        // ��������ӳ�ʼ������...  
    }

    public List<Recipe> Recipedata = new List<Recipe>();
    void Start()
    {
        // ����JSON�ļ�����Resources�ļ�����  
        //string jsonPath = Path.Combine(Application.streamingAssetsPath, "recipes.json");
        //string jsonData = File.ReadAllText(jsonPath);
        //recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonData); //JsonUtility.FromJson<List<Recipe>>(jsonData);
    }
}
