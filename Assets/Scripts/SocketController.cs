using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SocketController : MonoBehaviour,IPointerClickHandler
{
    public bool State { get; private set; } = false;
    public Vector2 Coordinate { get; private set; }
   
  
    private Image _image;
    private Color _origColor;
    private int _numAliveNeighbour = 0;
    private List<SocketController> _neigbour = new List<SocketController>();
    public bool IsChecked { get; set; } = false;

    [SerializeField]
    private Color lifeColor = new Color(0,255,0,255);


    void Start()
    {
        _image = GetComponent<Image>();
        _origColor = _image.color;
    }

    void Update()
    {
        if (IsChecked == false)
        {
            CheckMainCondition();
            IsChecked = true;
        }

    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        SetSate(!State);
    }

    public void InitCoordinate(int row,int column)
    {
        Coordinate = new Vector2(row, column);
    }

    public void AddNeighbour(SocketController obj)
    {
        _neigbour.Add(obj);
    }

    public void SetSate(bool state)
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

    public void CheckAlivegNeighbour()
    {
        _numAliveNeighbour = 0;
        foreach (var element in _neigbour)
        {
            if (element.State == true)
            {
                _numAliveNeighbour++;
            }
        }
    }

    void CheckMainCondition()
    {
        if (_numAliveNeighbour == 3)
        {
             SetSate(true);

        } else if (_numAliveNeighbour < 2 || _numAliveNeighbour > 3)
        {

            SetSate(false);

        }
    }
}
