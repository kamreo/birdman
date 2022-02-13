using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class EnemyMovement : MonoBehaviour
{

    private Transform target;

    [SerializeField, Range(0, 15)]
    private float targetDistance = 5.0f;
    [SerializeField]
    private LayerMask targetLayerMask, raycastLayerMask;

    private EntityMovement entityMovement;
    [SerializeField]
    private Vector2 move = Vector2.zero;

    [SerializeField]
    private EnemyType enemyType = EnemyType.Circle;

    private void OnEnable()
    {
        entityMovement = GetComponent<EntityMovement>();
    }

    private void Update()
    {
        UpdateInput();
        UpdatePath();
        UpdateEntityMovement();
    }

    private void UpdateInput()
    {
        if (this.target)
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, targetDistance * 2, targetLayerMask);
            if (!target)
                this.target = null;
        }
        else
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, targetDistance, targetLayerMask);
            if (target)
            {
                RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, target.transform.position - transform.position, targetDistance, raycastLayerMask);
                if (raycastHit && raycastHit.transform == target.transform)
                {
                    this.target = target.transform;
                }
            }
                
        }
    }

    private void UpdatePath()
    {
        if (!target)
        {
            move = Vector2.zero;
            return;
        }

        switch (enemyType)
        {
            case EnemyType.Circle:
                return;
            case EnemyType.Square:
                move = target.position - transform.position;
                return;
        }
    }

    private void UpdateEntityMovement()
    {
        if (!entityMovement)
            return;
        entityMovement.SetMove(move);
    }

    public enum EnemyType
    {
        Square,
        Circle
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, targetDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetDistance * 2);
    }

}