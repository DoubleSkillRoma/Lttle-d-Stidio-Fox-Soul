using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private const float _targetYPosition = -15.0f;
    private Vector2 _newPlayerPosition = new(-22.86f, -1.302f);
    
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] GameObject _ballPrefab;   

    private Teleport _teleport;

    private void Start()
    {
        _teleport =FindObjectOfType<Teleport>();
    }
    private void Update()
    {
        GroundGhecking();
    }
    private void GroundGhecking()
    {
        GameObject player = _playerPrefab; ;
        if (player != null)
        {
            if (player.transform.position.y <= _targetYPosition)
            {
                player.transform.position = _newPlayerPosition;
            }
        }
        
        GameObject ball = _ballPrefab;
        if (ball != null)
        {
            if(ball.transform.position.y <= _targetYPosition)
            {
                _teleport.SpawnBall(player.transform.position);
                _teleport.CheckDistanceAndCreateBall();
            }
        }
    }
}


