using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera; // ���������
    private Vector3 screenPoint; // �������Ļ�ϵĳ�ʼλ��
    private Vector3 offset; // ����������ռ��еĳ�ʼƫ����
    private bool isCardDragging = false; // �Ƿ�������ק
    private RaycastHit hit;
    private Vector3 m_prevPosition;
    private Vector3 targetPostion;

    private GameObject ObjectToMove;
    void Start()
    {
        mainCamera = Camera.main; // ��ȡ�����
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
                    //��¼��ʼλ�� �����Ҽ�ȡ������
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
                // ���������λ��
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

