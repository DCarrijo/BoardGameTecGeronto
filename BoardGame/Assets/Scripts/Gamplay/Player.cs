using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
using DG.Tweening;

public class Player
{
    public int PlayerId { get; private set; }
    public GameObject PlayerSpaceShip { get; private set; }

    public TilesGraph CurentTile { get; private set; } = null;

    public bool FinishedMooving { get; private set; } = false;

    public int LastRouteTaken { get; private set; } = 0;

    private PlayerParameters _playerParameters;

    public static Action OnMultipleRouts;

    public Player(int playerId, GameObject playerSpaceShip, TilesGraph curentTile, PlayerParameters parameters)
    {
        this.PlayerId = playerId;
        this.PlayerSpaceShip = playerSpaceShip;
        this._playerParameters = parameters;
        
        if (curentTile != null)
        {
            this.CurentTile = curentTile;
            PlayerSpaceShip.transform.position = CurentTile.transform.position + Vector3.up;
        }
    }

    public IEnumerator MovePlayerForward(int spaces)
    {
        FinishedMooving = false;

        for (int i = 0; i < spaces; i++)
        {
            if (CurentTile.HasMultipleRoutes)
            {
                OnMultipleRouts?.Invoke();
                yield return new WaitUntil(()=>DirectionChooser.HasChoosenDirection);
                this.LastRouteTaken = DirectionChooser.ChoosenDirection;
                DirectionChooser.ResetParams();
                yield return new DOTweenCYInstruction.WaitForCompletion(MovePlayerToTile(CurentTile.GetConnectedTile(LastRouteTaken, ConnectionType.forward)));
            }
            else
            {
                yield return new DOTweenCYInstruction.WaitForCompletion(MovePlayerToTile(CurentTile.GetConnectedTile(0, ConnectionType.forward)));
            }
        }
        
        yield return new WaitForSeconds(0.5f);

        FinishedMooving = true;
    }

    public IEnumerator MovePlayerBackWards(int spaces)
    {
        FinishedMooving = false;
        
        for (int i = 0; i < spaces; i++)
        {
            yield return new DOTweenCYInstruction.WaitForCompletion(MovePlayerToTile(CurentTile.GetConnectedTile(LastRouteTaken, ConnectionType.backward)));
        }
        
        yield return new WaitForSeconds(0.5f);

        FinishedMooving = true;
    }

    private Tween MovePlayerToTile(TilesGraph tile)
    {
        if (tile == null)
            return null;

        CurentTile = tile;
        
        return PlayerSpaceShip.transform.DOMove(tile.transform.position + Vector3.up, _playerParameters.MovementDuretion).SetEase(_playerParameters.MovementAnimationCurve);
        //PlayerSpaceShip.transform.position = tile.transform.position + Vector3.up;
    }

    public void MovePlayerInTile(Vector3 vectorRelativeToCenter)
    {
        PlayerSpaceShip.transform.position = CurentTile.transform.position + Vector3.up + vectorRelativeToCenter;
    }
}
