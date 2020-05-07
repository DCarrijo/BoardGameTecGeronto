using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonPoints : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = new Vector3(1.5f, 0f, 0f);
    [SerializeField] private float _intialAngle = 0f;

    private Vector3[] _points = null;
    public Vector3[] Points => _points;

    private void Awake()
    {
        CalculatePoints();
    }

    private void OnEnable()
    {
        CalculatePoints();
    }

    public void CalculatePoints()
    {
        _points = new Vector3[6];
        
        for (int i = 0; i < 6; i++)
        {
            _points[i] = RotatePointAroundPivot( (this.transform.position + _offset),this.transform.position, new Vector3(0, 60f * i +_intialAngle, 0f));
        }
    }

    private void OnDrawGizmos()
    {
        CalculatePoints();

        for (int i = 0; i < 6; i++)
        {
            Gizmos.color = i == 0 ? Color.green : Color.red;
            Gizmos.DrawWireSphere(_points[i], 0.1f);
        }
    }
    
    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; 
        dir = Quaternion.Euler(angles) * dir; 
        point = dir + pivot; 
        return point; 
    }
} 
