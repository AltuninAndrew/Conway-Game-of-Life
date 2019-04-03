using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SocketController : MonoBehaviour,IPointerClickHandler
{
    public bool State { get; private set; } = false;
    public Vector2 Coordinate { get; private set; }

    public int NumLivingNeighbour = 0;
    public bool IsCheck { get; set; } = true;

    private Image _image;
    private Color _origColor;

    [SerializeField]
    private Color lifeColor = new Color(0,255,0,255);

    public static explicit operator SocketController(GameObject v)
    {
        try
        {
            return v.GetComponent<SocketController>();
        }
        catch
        {
            return null;
        }
       
    }

    void Start()
    {
        _image = GetComponent<Image>();
        _origColor = _image.color;
    }
        
    public void OnPointerClick(PointerEventData eventData)
    {
        SetSate(!State);
    }

    void SetSate(bool state)
    {
        State = state;

        if (State == true)
        {
            _image.color = lifeColor;
           
        }
        else
        {
            _image.color = _origColor;
        }
        

    }

    public void InitCoordinate(int row,int column)
    {
        Coordinate = new Vector2(row, column);
    }

    void Update()
    {
        if(IsCheck == false)
        {
            
            if (NumLivingNeighbour == 3)
            {
                SetSate(true);
            }

            if (NumLivingNeighbour < 2 || NumLivingNeighbour > 3)
            {
                SetSate(false);
            }
            IsCheck = true;
        }
       
       
        
    }





}
