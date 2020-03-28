using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof (Selectable))]
public class InputFieldManager : InputField, IPointerClickHandler
{
    public InputField field;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        field = this;
        field.GetComponent<RectTransform>().localPosition = new Vector3(0, 262, 0);
        field.transform.parent.GetChild(0).gameObject.SetActive(false);
    }
}
