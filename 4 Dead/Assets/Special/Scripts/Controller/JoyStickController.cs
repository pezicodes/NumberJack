using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] RectTransform joystick_OutLine;
    [SerializeField] RectTransform joystick_Handle;
    [SerializeField] float move_Speed = 3.5f;

    #region Private variable
    private float radious;
    private bool isTouch = false;
    private GameObject player;
    #endregion

    #region Public variable
    public Vector3 move_Vec;
    public Vector2 value;
    #endregion

    #region LifeCycle
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        radious = joystick_OutLine.rect.width * 0.5f;
    }

    private void Update()
    {
        if (isTouch)
        {
            player.transform.position += move_Vec;
            if (value != null)
            {
                player.transform.rotation = Quaternion.Euler(0.0f, Mathf.Atan2(value.x, value.y) * Mathf.Rad2Deg, 0.0f);
            }
        }
    }
    #endregion

    #region IEvent
    /// <summary>
    /// 배경 터치 시 해당위치로 조이스틱 핸들 이동처리
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        value = eventData.position - (Vector2)joystick_OutLine.position;
        value = Vector2.ClampMagnitude(value, radious);

        joystick_Handle.localPosition = value;

        value = value.normalized;

        move_Vec = new Vector3(value.x * move_Speed * Time.deltaTime, 0.0f, value.y * move_Speed * Time.deltaTime);   
    }

    /// <summary>
    /// 터치 중일 때 OnDrag함수에 eventData를 인수로 삼아 처리
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
        OnDrag(eventData);
    }

    /// <summary>
    /// 터치 중 손을 떼었을 경우에 조이스틱 핸들 위치 처리
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        joystick_Handle.localPosition = Vector3.zero;

        isTouch = false;

        move_Vec = Vector3.zero;
    }
    #endregion
}
