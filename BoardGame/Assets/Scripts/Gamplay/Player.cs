using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

public class Player
{
    public int PlayerId { get; private set; }
    public GameObject PlayerSpaceShip { get; private set; }

    public TilesGraph CurentTile { get; private set; } = null;

    public bool FinishedMooving { get; private set; } = false;

    public int LastRouteTaken { get; private set; } = -1;

    public Player(int playerId, GameObject playerSpaceShip, TilesGraph curentTile)
    {
        this.PlayerId = playerId;
        this.PlayerSpaceShip = playerSpaceShip;
        
        if (curentTile != null)
        {
            MovePlayerToTile(curentTile);
        }
    }

    public IEnumerator MovePlayerForward(int spaces)
    {
        FinishedMooving = false;

        for (int i = 0; i < spaces; i++)
        {
            MovePlayerToTile(CurentTile.GetConnectedTile(0, ConnectionType.forward));
            yield return null;
        }
        
        yield return new WaitForSeconds(0.5f);

        FinishedMooving = true;
    }

    public IEnumerator MovePlayerBackWards(int spaces)
    {
        FinishedMooving = false;
        
        for (int i = 0; i < spaces; i++)
        {
            MovePlayerToTile(CurentTile.GetConnectedTile(0, ConnectionType.backward));
            yield return null;
        }
        
        yield return new WaitForSeconds(0.5f);

        FinishedMooving = true;
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
