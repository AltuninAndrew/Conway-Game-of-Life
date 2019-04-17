using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{

    private Dictionary<Vector2, SocketController> _sockets = new Dictionary<Vector2, SocketController>();
    private int _columnCount = 0;
    private int _rowCount = 0;
    private bool _isEnableLoop = false;

    [SerializeField]
    private GameObject prefabPanel = null;

    [SerializeField]
    private int numSocket = 210;

    [SerializeField]
    private float defaultSpeed = 0.1f;

    private float speed;
   

    void Start()
    {
        speed = defaultSpeed;
        _columnCount = GetComponent<GridLayoutGroup>().constraintCount;
        _rowCount = numSocket / _columnCount;
        InitField();
        InitNeighbourInSocket();
    }

    void InitField()
    {
        for (int i = 0; i < numSocket; i++)
        {
            SocketController socket = (SocketController)Instantiate(prefabPanel, gameObject.transform);
            socket.InitCoordinate(i / (_columnCount), (i % _columnCount));
            socket.name = "E[" + i / (_columnCount) + "][" + (i % _columnCount) + "]";
            _sockets.Add(socket.Coordinate, socket);
        }

    }

    void InitNeighbourInSocket()
    {
        foreach (var element in _sockets.Values)

        {
            //right
            if (element.Coordinate.y + 1 < _columnCount)
            {
                element.AddNeighbour(_sockets[new Vector2(element.Coordinate.x, element.Coordinate.y + 1)]);
            }

            //left
            if (element.Coordinate.y > 0)
            {
                element.AddNeighbour(_sockets[new Vector2(element.Coordinate.x, element.Coordinate.y - 1)]);
            }

            //up
            if (element.Coordinate.x > 0)
            {
                element.AddNeighbour(_sockets[new Vector2(element.Coordinate.x - 1, element.Coordinate.y)]);
            }

            //down
            if (element.Coordinate.x + 1 < _rowCount)
            {
                element.AddNeighbour(_sockets[new Vector2(element.Coordinate.x + 1, element.Coordinate.y)]);
            }

            //up left
            if ((element.Coordinate.x > 0) && (element.Coordinate.y > 0))
            {
                element.AddNeighbour(_sockets[new Vector2(element.Coordinate.x - 1, element.Coordinate.y - 1)]);
            }

            //up right
            if (((element.Coordinate.x > 0) && (element.Coordinate.y + 1 < _columnCount)))
            {
                element.AddNeighbour(_sockets[new Vector2(element.Coordinate.x - 1, element.Coordinate.y + 1)]);
            }

            //down right
            if ((element.Coordinate.x + 1 < _rowCount) && (element.Coordinate.y + 1 < _columnCount))
            {
                element.AddNeighbour(_sockets[new Vector2(element.Coordinate.x + 1, element.Coordinate.y + 1)]);
            }

            //down left
            if ((element.Coordinate.x + 1 < _rowCount) && (element.Coordinate.y > 0))
            {
                element.AddNeighbour(_sockets[new Vector2(element.Coordinate.x + 1, element.Coordinate.y - 1)]);
            }
        }
    }

    public void Step()
    {
        foreach(var element in _sockets.Values)
        {
            element.CheckAlivegNeighbour();
            element.IsChecked = false;
        }
    }

    IEnumerator Loop()
    {
        while(_isEnableLoop)
        {
            yield return new WaitForSeconds(speed);
            Step();
        }
    }

    public void StartWhile()
    {
        _isEnableLoop = !_isEnableLoop;
        StartCoroutine(Loop());
    }

    public void CleanPanel()
    {
        foreach(var element in _sockets.Values)
        {
            element.SetSate(false);
        }
    }

    public void ChangeSpeed(Slider slider)
    {
        speed = defaultSpeed / slider.value;
    }
}
