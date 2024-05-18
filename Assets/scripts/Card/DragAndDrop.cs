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
                // �����������Ļ�ϵ�λ��
                Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                //Debug.Log(Input.mousePosition);
                // ����Ļ����ת��Ϊ��������
                Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(cursorPoint) + offset;
                //cursorPosition.z = -1;
                // ���������λ��
                Card CardObj = ObjectToMove.GetComponent<Card>();
                CardObj.Move(cursorPosition);
                //Debug.Log(cursorPosition);
            }
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // �����������Ļ�ϵ�λ��
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            //Debug.Log(Input.mousePosition);
            // ����Ļ����ת��Ϊ��������
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
                // ���������λ��
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

