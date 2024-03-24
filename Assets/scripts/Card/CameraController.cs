using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //private Vector3 offset; // ���ڴ洢������������ʼλ�õ�ƫ����
    //private bool isDragging = false; // �Ƿ�������ק�ı�־
    //
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0)) // ������������ʱ
    //    {
    //        isDragging = true; // ������ק��־Ϊtrue
    //        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane / 2f));
    //    }
    //
    //    if (Input.GetMouseButtonUp(0)) // ���������ͷ�ʱ
    //    {
    //        isDragging = false; // ������ק��־Ϊfalse
    //    }
    //
    //    if (isDragging) // ���������ק
    //    {
    //        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane / 2f);
    //        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
    //        transform.position = cursorPosition; // ���������λ��
    //    }
    //}
    private Vector3 m_prevPosition;
    public float m_cameraScaleVal;
    public float m_minCamYPos, m_maxCamYPos;
    public float m_minCamXPos, m_maxCamXPos;
    void Update()
    {
        //if (GameRoot.Instance.isMenuActive)
        //{
        //    return;
        //}

#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
			HandleTouchInput();
#else
        HandleMouseInput();
#endif
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                m_prevPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 curPosition = touch.position;
                MoveCamera(m_prevPosition, curPosition);
                m_prevPosition = curPosition;
            }
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_prevPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 curMousePosition = Input.mousePosition;
            MoveCamera(m_prevPosition, curMousePosition);
            m_prevPosition = curMousePosition;
        }
    }

    private void MoveCamera(Vector2 prevPosition, Vector2 curPosition)
    {
        //ע�������myCamera.nearClipPlaen��������ʹ�õ���͸�������������Ҫ��zֵ��Ϊ���
        //�������ʹ�õ���������������ܲ���Ҫ���
        Vector2 offset = (Camera.main.ScreenToWorldPoint(new Vector3(prevPosition.x, prevPosition.y, Camera.main.nearClipPlane))
            - Camera.main.ScreenToWorldPoint(new Vector3(curPosition.x, curPosition.y, Camera.main.nearClipPlane)));
        //�����m_cameraScale,��Ϊ�Ҳ����޸�nearClipPlaen��ֵ���ﵽ�ƶ��Ŀ��������Լ��˸��ƶ�����
        Vector2 newPos = new Vector2(transform.localPosition.x + offset.x * m_cameraScaleVal, transform.localPosition.y + offset.y * m_cameraScaleVal);
        newPos.y = Mathf.Clamp(newPos.y, m_minCamYPos, m_maxCamYPos);
        newPos.x = Mathf.Clamp(newPos.x, m_minCamXPos, m_maxCamXPos);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
