using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Atom
{
    public class OKWindowController : WindowController, IPointerDownHandler, IDragHandler
    {

        [SerializeField]
        Text titleText;
        [SerializeField]
        Text contentText;

        Vector2 pressPointerOffset;
        RectTransform rectTransform;
        RectTransform parentRectTransform;

        void Awake()
        {
            //Debug.Log("OKWindowController.Awake!");
            //ProgramEventManager.GetInstance().AddLocalEventListener(gameObject, "OnWindowOpen", new ProgramEventManager.EventListener(OnWindowOpen));
        }

        //void OnEnable()
        //{
        //    titleText.text = "";
        //    contentText.text = "";
        //}

        // Use this for initialization
        void Start()
        {
            //Debug.Log("OKWindowController.Start!");
            rectTransform = this.GetComponent<RectTransform>();
            parentRectTransform = (RectTransform)rectTransform.parent;
            
        }

        // Update is called once per frame
        //void Update()
        //{

        //}

        //void OnWindowOpen(object data)
        //{
        //    //Debug.Log("OnWindowOpen");
        //    if (data != null)
        //    {
            
        //    }

        //}

        public override void Open(object context)
        {
            Dictionary<string, object> dataValue = (Dictionary<string, object>)context;
            titleText.text = (string)dataValue["titleText"];
            contentText.text = (string)dataValue["contentText"];
			transform.localPosition = Vector2.zero;
            base.Open(context);
        }

        public void OnClickOKBtn()
        {
            //Debug.Log("OnClickOKBtn!");
            //gameObject.SetActive(false);
            base.Close();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("OnPointerDown!");
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, eventData.position, eventData.pressEventCamera, out pressPointerOffset);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pointerPosition;
            //if(RectTransformUtility.ScreenPointToLocalPointInRectangle(this.parentRectTransform,eventData.position, eventData.pressEventCamera, out pointerPosition))
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.parentRectTransform, Input.mousePosition, eventData.pressEventCamera, out pointerPosition))
            {
                this.rectTransform.localPosition = pointerPosition - pressPointerOffset;
            }
        }

        //void OnDestroy()
        //{
        //    ProgramEventManager.GetInstance().RemoveLocalEventListener(gameObject, "OnWindowOpen", new ProgramEventManager.EventListener(OnWindowOpen));
        //}
    }
}

