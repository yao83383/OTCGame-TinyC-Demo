using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera; // 引用主相机
    private Vector3 screenPoint; // 鼠标在屏幕上的初始位置
    private Vector3 offset; // 物体在世界空间中的初始偏移量
    private bool isCardDragging = false; // 是否正在拖拽
    private RaycastHit hit;
    private Vector3 m_prevPosition;

    private GameObject ObjectToMove;
    void Start()
    {
        mainCamera = Camera.main; // 获取主相机
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
                if (hit.collider.transform.parent)
                {
                    ObjectToMove = hit.collider.transform.parent.gameObject;
                }
                else
                {
                    ObjectToMove = hit.collider.transform.gameObject;
                }

                m_prevPosition = Input.mousePosition;
                if (hit.collider.gameObject.CompareTag("Card"))
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
                //cursorPosition.z = -1;
                // 更新物体的位置
                Card CardObj = ObjectToMove.GetComponent<Card>();
                CardObj.Move(cursorPosition);
                //Debug.Log(cursorPosition);
            }
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 计算鼠标在屏幕上的位置
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            //Debug.Log(Input.mousePosition);
            // 将屏幕坐标转换为世界坐标
            Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(cursorPoint) + offset;
            //cursorPosition.z = -1;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Card"))
                {
                    Card TargetCard = hit.collider.transform.parent.gameObject.GetComponent<Card>().GetListEnd();
                    Card CardObj = ObjectToMove.GetComponent<Card>();
                    CardObj.Drop(TargetCard, TargetCard.transform.position + TargetCard.NextCardOffset);
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
        }
        else
        {
            ObjectToMove = null;
            isCardDragging = false;
        }
    }
}

