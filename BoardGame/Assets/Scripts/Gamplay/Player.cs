using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void MovePlayerForward(int spaces)
    {
        for (int i = 0; i < spaces; i++)
        {
            MovePlayerToTile(CurentTile.GetConnectedTile(0, ConnectionType.forward));
        }
    }

    public void MovePlayerBackWards(int spaces)
    {
        for (int i = 0; i < spaces; i++)
        {
            MovePlayerToTile(CurentTile.GetConnectedTile(0, ConnectionType.backward));
        }
    }

    private void MovePlayerToTile(TilesGraph tile)
    {
        if (tile == null)
            return;
        
        CurentTile = tile;
        PlayerSpaceShip.transform.position = tile.transform.position + Vector3.up;
    }

    public void MovePlayerInTile(Vector3 vectorRelativeToCenter)
    {
        PlayerSpaceShip.transform.position = CurentTile.transform.position + Vector3.up + vectorRelativeToCenter;
    }
}
