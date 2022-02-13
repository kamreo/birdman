using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class PlayerMovement : MonoBehaviour
{

    private EntityMovement entityMovement;
    private bool sprint = false;
    private Vector2 move = Vector2.zero;

    private void OnEnable()
    {
        entityMovement = GetComponent<EntityMovement>();
    }

    private void Update()
    {
        UpdateInput();
        UpdateEntityMovement();
    }

    private void UpdateInput()
    {
        sprint = Input.GetKey(KeyCode.LeftShift);
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
    }

    private void UpdateEntityMovement()
    {
        if (!entityMovement)
            return;
        entityMovement.SetSprint(sprint);
        entityMovement.SetMove(move);
    }

}
