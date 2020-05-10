using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Player
{
    public int PlayerId { get; private set; }
    public GameObject PlayerSpaceShip { get; private set; }

    public TilesGraph CurentTile { get; private set; } = null;

    public Player(int playerId, GameObject playerSpaceShip, TilesGraph curentTile)
    {
        this.PlayerId = playerId;
        this.PlayerSpaceShip = playerSpaceShip;
        
        if (curentTile != null)
        {
            MovePlayerToTile(curentTile);
        }
    }

    public void MovePlayerToTile(TilesGraph tile)
    {
        CurentTile = tile;
        PlayerSpaceShip.transform.position = tile.transform.position + Vector3.up;
    }

    public void MovePlayerInTile(Vector3 vectorRelativeToCenter)
    {
        PlayerSpaceShip.transform.position = CurentTile.transform.position + Vector3.up + vectorRelativeToCenter;
    }
    
}
