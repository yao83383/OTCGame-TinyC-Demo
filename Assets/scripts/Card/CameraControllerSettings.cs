using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerSettings : MonoBehaviour
{
    //private Vector3 offset; // 用于存储相机相对于鼠标初始位置的偏移量
    //private bool isDragging = false; // 是否正在拖拽的标志
    //
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0)) // 当鼠标左键按下时
    //    {
    //        isDragging = true; // 设置拖拽标志为true
    //        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane / 2f));
    //    }
    //
    //    if (Input.GetMouseButtonUp(0)) // 当鼠标左键释放时
    //    {
    //        isDragging = false; // 设置拖拽标志为false
    //    }
    //
    //    if (isDragging) // 如果正在拖拽
    //    {
    //        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane / 2f);
    //        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
    //        transform.position = cursorPosition; // 更新相机的位置
    //    }
    //}
    private Vector3 m_prevPosition;
    public float m_cameraScaleVal;
    public float m_minCamYPos, m_maxCamYPos;
    public float m_minCamXPos, m_maxCamXPos;

//    private RaycastHit hit;

//    void Update()
//    {
//        //if (GameRoot.Instance.isMenuActive)
//        //{
//        //    return;
//        //}

//#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
//			HandleTouchInput();
//#else
//        HandleMouseInput();
//#endif
//    }

//    void HandleTouchInput()
//    {
//        if (Input.touchCount == 1)
//        {
//            Touch touch = Input.GetTouch(0);
//            if (touch.phase == TouchPhase.Began)
//            {
//                m_prevPosition = touch.position;
//            }
//            else if (touch.phase == TouchPhase.Moved)
//            {
//                Vector2 curPosition = touch.position;
//                MoveCamera(m_prevPosition, curPosition);
//                m_prevPosition = curPosition;
//            }
//        }
//    }

//    void HandleMouseInput()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out hit))
//            {
//                if (hit.collider.gameObject.CompareTag("Map"))
//                {
//                    m_prevPosition = Input.mousePosition;
//                    Debug.Log("ClickDown on Map ");
//                }
//            }
//        }
//        else if (Input.GetMouseButton(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out hit))
//            {
//                if (hit.collider.gameObject.CompareTag("Map"))
//                {
//                    Vector2 curMousePosition = Input.mousePosition;
//                    MoveCamera(m_prevPosition, curMousePosition);
//                    m_prevPosition = curMousePosition;
//                    Debug.Log("ClickHold on Map " + hit.collider.gameObject.name);
//                }
//            }
//        }
//    }

//    private void MoveCamera(Vector2 prevPosition, Vector2 curPosition)
//    {
//        //注意这里的myCamera.nearClipPlaen。由于我使用的是透视相机，所以需要将z值改为这个
//        //如果读者使用的是正交相机，可能不需要这个
//        Vector2 offset = (Camera.main.ScreenToWorldPoint(new Vector3(prevPosition.x, prevPosition.y, Camera.main.nearClipPlane))
//            - Camera.main.ScreenToWorldPoint(new Vector3(curPosition.x, curPosition.y, Camera.main.nearClipPlane)));
//        //这里的m_cameraScale,因为我不想修改nearClipPlaen的值来达到移动的快慢，所以加了个移动参数
//        Vector2 newPos = new Vector2(transform.localPosition.x + offset.x * m_cameraScaleVal, transform.localPosition.y + offset.y * m_cameraScaleVal);
//        newPos.y = Mathf.Clamp(newPos.y, m_minCamYPos, m_maxCamYPos);
//        newPos.x = Mathf.Clamp(newPos.x, m_minCamXPos, m_maxCamXPos);
//        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
//    }
}
