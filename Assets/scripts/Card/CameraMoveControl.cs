
using UnityEngine;
 
/// <summary>
/// 
/// * Writer：June
/// *
/// * Data：2021.3.9
/// *
/// * Function：RTS模式的相机移动
/// *
/// * Remarks：
/// 
/// </summary>
 
public class CameraMoveControl : MonoBehaviour
{
    #region 移动
    /// <summary>
    /// 移动速度
    /// </summary>
    private float panSpeed;
    /// <summary>
    /// 正常速度
    /// </summary>
    [SerializeField] private float normalSpeed;
    /// <summary>
    /// 按shift加速
    /// </summary>
    [SerializeField] private float speedUp;
    /// <summary>
    /// 缓冲时间
    /// </summary>
    [SerializeField] private float moveTime;
    private Vector3 newPos;
    /// <summary>
    /// 边界限制
    /// </summary>
    [SerializeField] private float xLimMin, xLimMax;
    /// <summary>
    /// 这里的Y是指屏幕上下平移的限制
    /// </summary>
    [SerializeField] private float yLimMin, yLimMax;
    //-----------------------------------------------鼠标拖动操作相关字段----------------------------------------------------
    private Camera mainCamrea;
    private Vector3 startPoint, currentPoint;
    #endregion
 
    #region 缩放
    /// <summary>
    /// 主摄像机的位置组件
    /// </summary>
    private Transform mainCamreaTF;
    /// <summary>
    /// 缩放向量
    /// tips:相机的放大缩小改变的是相机自身坐标的yz值
    /// </summary>
    [SerializeField] private Vector3 zoomV3;
    /*
     * 需要注意的是缩放限制：
     * x轴与y轴限制后的缩放比值要一致，不然会出现缩放不平滑的现象
     * 
     */
    /// <summary>
    /// 缩放最大最小值
    /// </summary>
    [SerializeField] private Vector3 zoomMin, zoomMax;
    private Vector3 newMainCamreaPos;
    /// <summary>
    /// 缩放时间
    /// </summary>
    [SerializeField] private float zoomTime;
    #endregion
 
    private void Start()
    {
        //判断是否有子物体
        mainCamreaTF = transform.childCount > 0 ? transform.GetChild(0) : null;
        if (mainCamreaTF) newMainCamreaPos = mainCamreaTF.localPosition;
        mainCamrea = Camera.main;
    }
 
 
    private void Update()
    {
        //按左shift加速
        panSpeed = Input.GetKey(KeyCode.LeftShift) ? speedUp : normalSpeed;
        //移动
        ControlCamreaMove();
        //缩放
        ControlCamreaZoom();
    }
 
    /// <summary>
    /// 控制相机缩放
    /// </summary>
    private void ControlCamreaZoom()
    {
        if (mainCamreaTF)
        {
            if (Input.GetKey(KeyCode.R)) newMainCamreaPos += zoomV3 * Time.deltaTime;//放大
            if (Input.GetKey(KeyCode.F)) newMainCamreaPos -= zoomV3 * Time.deltaTime;//缩小
            newMainCamreaPos -= Input.GetAxis("Mouse ScrollWheel") * zoomV3;
            ZoomLimit(ref newMainCamreaPos);
            //刷新最终位置
            mainCamreaTF.localPosition = Vector3.Lerp(mainCamreaTF.localPosition, newMainCamreaPos, zoomTime * Time.deltaTime);
        }
    }
 
 
    /// <summary>
    /// 控制相机移动
    /// </summary>
    private void ControlCamreaMove()
    {
        Vector3 movePos = transform.position;
        newPos.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
 
        #region 鼠标操作
        #region 方式1（鼠标到达边缘，检测后操作相机移动）
        // Vector2 mousePos = Input.mousePosition;
        // //鼠标在四个边缘检测
        // if (mousePos.x > Screen.width * 0.9f && mousePos.x < Screen.width) newPos.x = 1;
        // if (mousePos.x < Screen.width * 0.1f && mousePos.x > 0) newPos.x = -1;
        // if (mousePos.y > Screen.height * 0.9f && mousePos.y < Screen.height) newPos.z = 1;
        // if (mousePos.y < Screen.height * 0.1f && mousePos.y > 0) newPos.z = -1;
 
        movePos += newPos.normalized * panSpeed * Time.deltaTime;
        #endregion
 
        #region 方式2（鼠标右键拖动控制相机移动）
        //首先判断相机是否为空
        if (mainCamrea)
        {
            //鼠标右键按下时记录起始位置
            if (Input.GetMouseButtonDown(1))
            {
                //新建的世界坐标系下的平面，用于检测射线
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = mainCamrea.ScreenPointToRay(Input.mousePosition);
                float distance;
                if (plane.Raycast(ray, out distance)) 
                {
                    //获取碰撞位置
                    startPoint = ray.GetPoint(distance);
                }
            }
            //鼠标右键一直按下时记录当前点位置
            if (Input.GetMouseButton(1))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = mainCamrea.ScreenPointToRay(Input.mousePosition);
                float distance;
                if (plane.Raycast(ray, out distance))
                {
                    currentPoint = ray.GetPoint(distance);
                }
                movePos += (startPoint - currentPoint);
            }
        }
        #endregion
        #endregion
 
        BoundaryLimit(ref movePos);
        transform.position = Vector3.Lerp(transform.position, movePos, moveTime);
    }
 
 
    /// <summary>
    /// 边界限制
    /// </summary>
    /// <param name="_pos">要限制的目标向量</param>
    private void BoundaryLimit(ref Vector3 _pos)
    {
        _pos.x = Mathf.Clamp(_pos.x, xLimMin, xLimMax);
        _pos.z = Mathf.Clamp(_pos.z, yLimMin, yLimMax);
    }
 
 
    /// <summary>
    /// 缩放限制
    /// </summary>
    /// <param name="_v3">要限制的目标向量</param>
    private void ZoomLimit(ref Vector3 _v3)
    {
        _v3.y = Mathf.Clamp(_v3.y, zoomMin.y, zoomMax.y);
        _v3.z = Mathf.Clamp(_v3.z, zoomMin.z, zoomMax.z);
    }
}