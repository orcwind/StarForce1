using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace ARPGTest.UI.FrameWork
{
    //����ί������
    public delegate void PointerEventHandler(PointerEventData eventData);
    public delegate void BaseEventHandler(BaseEventData eventData);
    public delegate void AxisEventHandler(AxisEventData eventData);
    /// <summary>
    /// UI�¼�����������������UGUI�¼����ṩ�¼������࣬���ӵ���Ҫ������UIԪ���ϣ����ڼ����û��Ĳ�����
    /// ������EventTrigger
    /// </summary>
    public class UIEventListener : MonoBehaviour,IPointerDownHandler,IPointerClickHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler,IInitializePotentialDragHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler,IScrollHandler,IUpdateSelectedHandler,ISelectHandler,IDeselectHandler,IMoveHandler,ISubmitHandler,ICancelHandler,IEventSystemHandler
    {
        //�����¼�
        public event PointerEventHandler PointerClick;
        public event PointerEventHandler PointerDown;
        public event PointerEventHandler PointerUp;
        public event PointerEventHandler PointerEnter;
        public event PointerEventHandler PointerExit;
        public event PointerEventHandler InitializePotentialDrag;
        public event PointerEventHandler BeginDrag;
        public event PointerEventHandler EndDrag;
        public event PointerEventHandler Drag;
        public event PointerEventHandler Drop;
        public event PointerEventHandler Scroll;
        public event BaseEventHandler UpdateSelected;
        public event BaseEventHandler Selected;
        public event BaseEventHandler Deselect;
        public event AxisEventHandler Move;
        public event BaseEventHandler Submit;
        public event BaseEventHandler Cancel;

        public static UIEventListener GetListener(Transform tf)
        {
            UIEventListener listener = tf.GetComponent<UIEventListener>();
            if (listener == null) listener=tf.gameObject.AddComponent<UIEventListener>();
            return listener;
        }

       //�̳нӿ�
        public void OnPointerDown(PointerEventData eventData)
        {
            //�����¼�
            if (PointerDown != null)
                PointerDown(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (PointerClick != null)
                PointerClick(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (PointerUp != null)
                PointerUp(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (PointerEnter != null)
                PointerEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (PointerExit != null)
                PointerExit(eventData);
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if(InitializePotentialDrag != null)
                InitializePotentialDrag(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
          if(BeginDrag != null) BeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(Drag != null) Drag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
           if(EndDrag != null) EndDrag(eventData);
        }

        public void OnDrop(PointerEventData eventData)
        {
           if(Drop != null) Drop(eventData);
        }

        public void OnScroll(PointerEventData eventData)
        {
            if(Scroll != null) Scroll(eventData);
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            if (UpdateSelected != null) UpdateSelected(eventData);
        }

        public void OnSelect(BaseEventData eventData)
        {
           if(Selected != null) Selected(eventData);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if(Deselect != null) Deselect(eventData);
        }

        public void OnMove(AxisEventData eventData)
        {
            if(Move != null) Move(eventData);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if(Submit != null) Submit(eventData);
        }

        public void OnCancel(BaseEventData eventData)
        {
            if(Cancel != null) Cancel(eventData);
        }
    }
}