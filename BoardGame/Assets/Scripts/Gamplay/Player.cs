using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
using DG.Tweening;
using UnityEngine.PlayerLoop;

public class Player
{
    public int PlayerId { get; private set; }
    public GameObject PlayerSpaceShip { get; private set; }
    
    public PlayerComponents PlayerComps { get; private set; }

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

        this.PlayerComps = playerSpaceShip.GetComponent<PlayerComponents>();
    }

    public IEnumerator MovePlayerForward(int spaces)
    {
        FinishedMooving = false;
        
        PlayerComps.StartTurbo();

        for (int i = 0; i < spaces; i++)
        {
            TilesGraph nextTile;
            if (CurentTile.HasMultipleRoutes)
            {
                OnMultipleRouts?.Invoke();
                yield return new WaitUntil(()=>DirectionChooser.HasChoosenDirection);
                this.LastRouteTaken = DirectionChooser.ChoosenDirection;
                DirectionChooser.ResetParams();

                nextTile = CurentTile.GetConnectedTile(LastRouteTaken, ConnectionType.forward);
            }
            else
            {
                nextTile = CurentTile.GetConnectedTile(0, ConnectionType.forward);
            }

            CurentTile.CurrentPlayers.Remove(this);
            CurentTile.ArrangePlayersInTile();

            yield return new DOTweenCYInstruction.WaitForCompletion(RotatePlayerSpaceShip(nextTile.transform));
            yield return new DOTweenCYInstruction.WaitForCompletion(MovePlayerToTile(nextTile));
            
            CurentTile.CurrentPlayers.Add(this);
            CurentTile.ArrangePlayersInTile();
        }

        PlayerComps.StopTurbo();

        FinishedMooving = true;
    }

    public IEnumerator MovePlayerBackWards(int spaces)
    {
        FinishedMooving = false;
        
        PlayerComps.StartTurbo();
        
        CurentTile.CurrentPlayers.Add(this);
        CurentTile.ArrangePlayersInTile();
        
        for (int i = 0; i < spaces; i++)
        {
            TilesGraph nextTile = CurentTile.GetConnectedTile(LastRouteTaken, ConnectionType.backward);
            yield return new DOTweenCYInstruction.WaitForCompletion(RotatePlayerSpaceShip(nextTile.transform));
            yield return new DOTweenCYInstruction.WaitForCompletion(MovePlayerToTile(nextTile));
        }
        
        CurentTile.CurrentPlayers.Add(this);
        CurentTile.ArrangePlayersInTile();

        PlayerComps.StopTurbo();

        FinishedMooving = true;
    }

    private Tween MovePlayerToTile(TilesGraph tile)
    {
        if (tile == null)
            return null;

        CurentTile = tile;
        
        return PlayerSpaceShip.transform.DOMove(tile.transform.position + (Vector3.up * _playerParameters.TileFloatHightMultiplier), _playerParameters.MovementDuretion).SetEase(_playerParameters.MovementAnimationCurve);
        //PlayerSpaceShip.transform.position = tile.transform.position + Vector3.up;
    }

    public Tween MovePlayerInTile(Vector3 vectorRelativeToCenter)
    {
        return PlayerSpaceShip.transform.DOMove(CurentTile.transform.position + (Vector3.up * _playerParameters.TileFloatHightMultiplier) + vectorRelativeToCenter, 0.5f);
    }

    private Tween RotatePlayerSpaceShip(Transform nextTile)
    {
        return PlayerSpaceShip.transform.DOLookAt(nextTile.position + (Vector3.up * _playerParameters.TileFloatHightMultiplier), _playerParameters.LookAtDuration).SetEase(_playerParameters.LookDirectionAnimationCurve);
    }
}
