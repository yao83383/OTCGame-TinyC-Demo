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
    private Vector3 targetPostion;

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

    void MoveCard(GameObject InObj, Vector3 InPos)
    {
        if (InObj)
        {
            Card CardObj = InObj.GetComponent<Card>();
            CardObj.Move(InPos);
        }
    }

    void DropCard(GameObject InObj, Vector3 InPos)
    {
        Card CardObj = InObj.GetComponent<Card>();
        if (CardObj)
        {
            CardObj.Drop(InPos);
        }
    }

    void GetRayTarget(ref Vector3 outTargetPostion)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain", "ClickableObject")))
        {
            outTargetPostion = hit.point + new Vector3(0, 1f, 0);
            //Debug.Log("hit postion:" + hit.point);
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("ClickableObject")))
            {
                if (hit.collider.transform.CompareTag("Card"))
                {
                    ObjectToMove = hit.collider.transform.parent.gameObject;
                    Card CardObj = ObjectToMove.GetComponent<Card>();
                    CardObj.StartMove();
                    isCardDragging = true;
                    //记录起始位置 用于右键取消操作
                    m_prevPosition = ObjectToMove.transform.position;
                    //Debug.Log("Click card, position:" + ObjectToMove.transform.position);
                    //Debug.Log("Click screenPoint:" + offset);
                }
                else
                {
                    ObjectToMove = hit.collider.transform.gameObject;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (isCardDragging)
            {
                targetPostion = new Vector3(0, 0, 0);
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                GetRayTarget(ref targetPostion);

                MoveCard(ObjectToMove, targetPostion);
            }
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (!isCardDragging) return;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("ClickableObject")))
            {
                if (hit.collider.gameObject.CompareTag("Card"))
                {
                    Card TargetCard = hit.collider.transform.parent.gameObject.GetComponent<Card>().GetListEnd();
                    Card CardObj = ObjectToMove.GetComponent<Card>();
                    CardObj.Drop(TargetCard);
                }
                else
                {
                    DropCard(ObjectToMove, m_prevPosition);
                }
            }
            else
            {
                if (!ObjectToMove) return;
                // 更新物体的位置
                DropCard(ObjectToMove, targetPostion);
            }

            ObjectToMove = null;
            isCardDragging = false;
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            if (isCardDragging)
            {
                DropCard(ObjectToMove, m_prevPosition);
            }
            ObjectToMove = null;
            isCardDragging = false;
        }
    }
}

