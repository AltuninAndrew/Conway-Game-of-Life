using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{

    private Dictionary<Vector2, SocketController> _sockets = new Dictionary<Vector2, SocketController>();
    private int columnCount = 0;
    private int rowCount = 0;
    private bool isEnableLoop = false;

    [SerializeField]
    private GameObject prefabPanel = null;

    [SerializeField]
    private int numSocket = 210;

    [SerializeField]
    private float speed = 1f;
   
    void Start()
    {
        columnCount = GetComponent<GridLayoutGroup>().constraintCount;
        rowCount = numSocket / columnCount;
        InitField();
    }

    void InitField()
    {
        for (int i = 0; i < numSocket; i++)
        {
            SocketController socket = (SocketController)Instantiate(prefabPanel, gameObject.transform);
            socket.InitCoordinate(i / (columnCount), (i % columnCount));
            socket.name = "E[" + i / (columnCount) + "][" + (i % columnCount) + "]";
            _sockets.Add(socket.Coordinate, socket);
        }

    }

    void InitNeigbourInSockets()
    {
        foreach (var element in _sockets.Values)
        {
           

            element.NumLivingNeighbour = 0;

            //right
            if ((element.Coordinate.y+1 < columnCount &&
                _sockets[new Vector2(element.Coordinate.x, element.Coordinate.y + 1)].State == true))
            {
                element.NumLivingNeighbour++;
            }

            //left
            if ((element.Coordinate.y > 0 &&
                _sockets[new Vector2(element.Coordinate.x, element.Coordinate.y - 1)].State == true))
            {
                element.NumLivingNeighbour++;
            }

            //up
            if ((element.Coordinate.x > 0 &&
                _sockets[new Vector2(element.Coordinate.x - 1, element.Coordinate.y)].State == true))
            {
                element.NumLivingNeighbour++;
            }

            //down
            if ((element.Coordinate.x+1 < rowCount &&
                _sockets[new Vector2(element.Coordinate.x + 1, element.Coordinate.y)].State == true))
            {
                element.NumLivingNeighbour++;
            }

            //up left
            if ((((element.Coordinate.x > 0) && (element.Coordinate.y > 0)) &&
                _sockets[new Vector2(element.Coordinate.x - 1, element.Coordinate.y - 1)].State == true))
            {
                element.NumLivingNeighbour++;
            }

            //up right
            if ((((element.Coordinate.x > 0) && (element.Coordinate.y + 1 < columnCount)) &&
                _sockets[new Vector2(element.Coordinate.x - 1, element.Coordinate.y + 1)].State == true))
            {
                element.NumLivingNeighbour++;
            }
            
            //down right
            if ((((element.Coordinate.x + 1 < rowCount) && (element.Coordinate.y + 1 < columnCount)) &&
                   _sockets[new Vector2(element.Coordinate.x + 1, element.Coordinate.y + 1)].State == true))
            {
                element.NumLivingNeighbour++;
            }

            //down left
            if ((((element.Coordinate.x + 1 < rowCount) && (element.Coordinate.y > 0)) &&
                _sockets[new Vector2(element.Coordinate.x + 1, element.Coordinate.y - 1)].State == true))
            {
                element.NumLivingNeighbour++;
            }

        }

    }

    IEnumerator Corutine()
    {
        while (isEnableLoop)
        {
            yield return new WaitForSeconds(speed);
            InitNeigbourInSockets();
            foreach (var element in _sockets.Values)
            {
                element.IsCheck = false;
            }
        }

    }

    public void Step()
    {
        InitNeigbourInSockets();
        foreach (var element in _sockets.Values)
        {
            element.IsCheck = false;
        }
        
    }

    public void StartWhile()
    {
        isEnableLoop = !isEnableLoop;
        StartCoroutine(Corutine());       
    }

   

}
