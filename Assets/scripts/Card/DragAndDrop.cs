using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera; // ���������
    private CameraControllerSettings CamCtrlSettings;
    private Vector3 screenPoint; // �������Ļ�ϵĳ�ʼλ��
    private Vector3 offset; // ����������ռ��еĳ�ʼƫ����
    private bool isCardDragging = false; // �Ƿ�������ק
    private bool isCameraDragging = false;
    private RaycastHit hit;
    private Vector3 m_prevPosition;

    private GameObject ObjectToMove;
    void Start()
    {
        mainCamera = Camera.main; // ��ȡ�����
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
                // �����������Ļ�ϵ�λ��
                Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                //Debug.Log(Input.mousePosition);
                // ����Ļ����ת��Ϊ��������
                Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(cursorPoint) + offset;
                cursorPosition.z = -1;
                // ���������λ��
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
            // �����������Ļ�ϵ�λ��
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            //Debug.Log(Input.mousePosition);
            // ����Ļ����ת��Ϊ��������
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
                // ���������λ��
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
        //ע�������myCamera.nearClipPlaen��������ʹ�õ���͸�������������Ҫ��zֵ��Ϊ���
        //�������ʹ�õ���������������ܲ���Ҫ���
        Vector2 offset = (Camera.main.ScreenToWorldPoint(new Vector3(prevPosition.x, prevPosition.y, Camera.main.nearClipPlane))
            - Camera.main.ScreenToWorldPoint(new Vector3(curPosition.x, curPosition.y, Camera.main.nearClipPlane)));
        //�����m_cameraScale,��Ϊ�Ҳ����޸�nearClipPlaen��ֵ���ﵽ�ƶ��Ŀ��������Լ��˸��ƶ�����
        Vector2 newPos = new Vector2(transform.localPosition.x + offset.x * CamCtrlSettings.m_cameraScaleVal, transform.localPosition.y + offset.y * CamCtrlSettings.m_cameraScaleVal);
        newPos.y = Mathf.Clamp(newPos.y, CamCtrlSettings.m_minCamYPos, CamCtrlSettings.m_maxCamYPos);
        newPos.x = Mathf.Clamp(newPos.x, CamCtrlSettings.m_minCamXPos, CamCtrlSettings.m_maxCamXPos);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}

