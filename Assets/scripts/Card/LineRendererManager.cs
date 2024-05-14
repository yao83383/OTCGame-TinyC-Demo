using UnityEngine;
using System.Collections;
 
public class LineRendererManager : MonoBehaviour {
    //LineRenderer
    private LineRenderer lineRenderer;
    //定义一个Vector3,用来存储鼠标点击的位置
    private Vector3 position;
    //用来索引端点
    private int index = 0;
    //端点数
    private int LengthOfLineRenderer=0;
 
    void Start()
    {
        //添加LineRenderer组件
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        //设置材质
        //string shaderName = "TextMeshPro/Shaders/TMP_Sprite";
        //Shader tempShader = Shader.Find(shaderName);
        //if (tempShader)
        { 
            Material mat = Resources.Load<Material>("Materials/card_green");
            lineRenderer.material = mat;
        }
        //设置颜色
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.yellow;
        //设置宽度
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;

        DrawGrid(GridSize);
    }

    public int GridLength = 10;
    public int GridSize = 10;
    public void DrawGrid(int size)
    {
        if (size <= 0) size = 1;
        int LeftTopGridNum = size / 2;
        int RightButtomGridNum = size - size / 2;
    }
 
    //void Update()
    //{  
    //    //获取LineRenderer组件
    //    lineRenderer = GetComponent<LineRenderer>();
    //   //鼠标左击
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //将鼠标点击的屏幕坐标转换为世界坐标，然后存储到position中
    //        position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,1.0f)); 
    //        //端点数+1
    //        LengthOfLineRenderer++;
    //        //设置线段的端点数
    //        lineRenderer.positionCount = LengthOfLineRenderer;
    //    }
    //    //连续绘制线段
    //    while (index < LengthOfLineRenderer)
    //    {   
    //        //两点确定一条直线，所以我们依次绘制点就可以形成线段了
    //        lineRenderer.SetPosition(index, position);
    //        index++;
    //    } 
    //}
 
    void OnGUI()
    {          
        GUILayout.Label("当前鼠标X轴位置：" + Input.mousePosition.x);
        GUILayout.Label("当前鼠标Y轴位置：" + Input.mousePosition.y);        
    }
}