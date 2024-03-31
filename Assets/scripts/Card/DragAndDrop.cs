using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera; // 引用主相机
    private CameraControllerSettings CamCtrlSettings;
    private Vector3 screenPoint; // 鼠标在屏幕上的初始位置
    private Vector3 offset; // 物体在世界空间中的初始偏移量
    private bool isCardDragging = false; // 是否正在拖拽
    private bool isCameraDragging = false;
    private RaycastHit hit;
    private Vector3 m_prevPosition;

    private GameObject ObjectToMove;
    void Start()
    {
        mainCamera = Camera.main; // 获取主相机
        CamCtrlSettings = this.GetComponent<CameraControllerSettings>();
    }

    void Update()
    {
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
			HandleTouchInput();
#else
        HandleMouseInput();
#endif
        
    }

    void HandleTouchInput()
    {
    }
    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                ObjectToMove = hit.collider.gameObject;
                m_prevPosition = Input.mousePosition;
                if (hit.collider.gameObject.CompareTag("Map"))
                {
                    isCameraDragging = true;
                    Debug.Log("Click Map");
                }
                else if (hit.collider.gameObject.CompareTag("Card"))
                {
                    screenPoint = mainCamera.WorldToScreenPoint(ObjectToMove.transform.position);
                    offset = ObjectToMove.transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                    Card CardObj = ObjectToMove.GetComponent<Card>();
                    CardObj.StartMove();
                    isCardDragging = true;
                    Debug.Log("Click Card");
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (isCardDragging)
            {
                // 计算鼠标在屏幕上的位置
                Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                //Debug.Log(Input.mousePosition);
                // 将屏幕坐标转换为世界坐标
                Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(cursorPoint) + offset;
                cursorPosition.z = -1;
                // 更新物体的位置
                Card CardObj = ObjectToMove.GetComponent<Card>();
                CardObj.Move(cursorPosition);
                //Debug.Log(cursorPosition);
            }
            else if (isCameraDragging)
            {
                Vector2 curMousePosition = Input.mousePosition;
                MoveCamera(m_prevPosition, curMousePosition);
                m_prevPosition = curMousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 计算鼠标在屏幕上的位置
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            //Debug.Log(Input.mousePosition);
            // 将屏幕坐标转换为世界坐标
            Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(cursorPoint) + offset;
            cursorPosition.z = -1;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Card"))
                {
                    Card TargetCard = hit.collider.gameObject.GetComponent<Card>().GetListEnd();
                    Card CardObj = ObjectToMove.GetComponent<Card>();
                    CardObj.Drop(TargetCard, TargetCard.transform.position - TargetCard.NextCardOffset);
                }
                else
                {
                    Card CardObj = ObjectToMove.GetComponent<Card>();
                    if (CardObj)
                    { 
                        CardObj.Drop(null, cursorPosition);
                    }
                }
            }
            else
            {
                // 更新物体的位置
                Card CardObj = ObjectToMove.GetComponent<Card>();
                if (CardObj)
                { 
                    CardObj.Drop(null, cursorPosition);
                }
            }
           
            ObjectToMove = null;
            isCardDragging = false;
            isCameraDragging = false;
        }
        else
        {
            ObjectToMove = null;
            isCardDragging = false;
            isCameraDragging = false;
        }
    }

    private void MoveCamera(Vector2 prevPosition, Vector2 curPosition)
    {
        //注意这里的myCamera.nearClipPlaen。由于我使用的是透视相机，所以需要将z值改为这个
        //如果读者使用的是正交相机，可能不需要这个
        Vector2 offset = (Camera.main.ScreenToWorldPoint(new Vector3(prevPosition.x, prevPosition.y, Camera.main.nearClipPlane))
            - Camera.main.ScreenToWorldPoint(new Vector3(curPosition.x, curPosition.y, Camera.main.nearClipPlane)));
        //这里的m_cameraScale,因为我不想修改nearClipPlaen的值来达到移动的快慢，所以加了个移动参数
        Vector2 newPos = new Vector2(transform.localPosition.x + offset.x * CamCtrlSettings.m_cameraScaleVal, transform.localPosition.y + offset.y * CamCtrlSettings.m_cameraScaleVal);
        newPos.y = Mathf.Clamp(newPos.y, CamCtrlSettings.m_minCamYPos, CamCtrlSettings.m_maxCamYPos);
        newPos.x = Mathf.Clamp(newPos.x, CamCtrlSettings.m_minCamXPos, CamCtrlSettings.m_maxCamXPos);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}

