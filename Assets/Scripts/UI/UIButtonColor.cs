using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class UIButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
{
    private Button _button;
    public Graphic targetGraphic;
    private ColorBlock _colorBlock;
    private bool _selected = false;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _colorBlock = _button.colors;
    }

    private void Start()
    {
        if(_button.interactable == false)
        {
            targetGraphic.color = _colorBlock.disabledColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_button.interactable == true)
        {
            if(_selected)
            {
                targetGraphic.color = _colorBlock.normalColor;
            }
            else
            {
                targetGraphic.color = _colorBlock.highlightedColor;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_button.interactable)
        {
            targetGraphic.color = _colorBlock.normalColor;
        }
        else
        {
            targetGraphic.color = _colorBlock.disabledColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            targetGraphic.color = _colorBlock.pressedColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            targetGraphic.color = _colorBlock.normalColor;
        }
        else
        {
            targetGraphic.color = _colorBlock.disabledColor;
        }
    }

    public void OnSelect(BaseEventData baseEventData)
    {
        _selected = true;
    }
    public void OnDeselect(BaseEventData baseEventData)
    {
        _selected = false;
    }

    public void SetColorNormal()
    {
        targetGraphic.color = _colorBlock.normalColor;
    }

    public void SetColorDisabled()
    {
        targetGraphic.color = _colorBlock.disabledColor;
    }
}