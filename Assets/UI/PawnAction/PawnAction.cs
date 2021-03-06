﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PawnAction : MonoBehaviour
{
    private Pawn selectedPawn;
    public GameInteraction gameInteraction;
    public HexMap hexMap;

    public float moveSpeed =1f;

    public enum Status { PrepareAttack, PrepareMove, Rest, IsMoving, IsAttacking,};

    public Status currentStatus = Status.Rest;

    private List<HexCell> routes;
    private int currentTarget;
    private bool validRoute;

    private Pawn attackTarget;
    private bool validAttackTarget;

    public void OnEnable()
    {
        currentStatus = Status.Rest;
    }

    public void SetPawn(Pawn pawn)
    {
        selectedPawn = pawn;
    }
    public void PrepareAttack()
    {
        if(selectedPawn != null)
        {
            Debug.Log(selectedPawn.Name + " is trying to attack");
            gameInteraction.IsPawnAction = true;
            currentStatus = Status.PrepareAttack;
            validAttackTarget = true;
            hexMap.ProbeAttackTarget(selectedPawn.currentCell);
            hexMap.ShowAttackCandidates();
        }
    }

    public void PrepareMove()
    {
        if(selectedPawn != null)
        {
            Debug.Log(selectedPawn.Name + " is trying to move");
            gameInteraction.IsPawnAction = true;
            currentStatus = Status.PrepareMove;
            validRoute = true;
            hexMap.FindReachableCells(selectedPawn.currentCell, selectedPawn.Dexterity);
            hexMap.ShowReachableCells();
        }
    }

    public void Move()
    {
        Debug.Log(selectedPawn.Name + " Moves");
        hexMap.HideIndicator();
        currentStatus = Status.IsMoving;

        UpdateRoot(selectedPawn, selectedPawn.currentCell, routes[routes.Count - 1]);
    }

    public void Attack()
    {
        Debug.Log(selectedPawn.Name + " Attacks" + attackTarget);
        hexMap.HideIndicator();
        currentStatus = Status.IsAttacking;
        selectedPawn.DoAttack(attackTarget);
    }

    public void Skip()
    {
        if(selectedPawn != null)
        {
            Debug.Log(selectedPawn.Name + " is skiping turn");
        }
    }

    public void Update()
    {
        if (currentStatus == Status.Rest)
            return;
        else if (currentStatus == Status.PrepareAttack)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                UpdateAttackTarget();
                if (validAttackTarget && (hexMap.IsReachable(getCurrentPointerCell()) || hexMap.IsReachable(getCurrentPointerPawn().currentCell)))
                {
                    Attack();
                }
                else
                {
                    ClearStatus();
                }
            }
        }
        else if(currentStatus == Status.PrepareMove)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                UpdateRoute();
                if (!validRoute || !hexMap.IsReachable(getCurrentPointerCell()))
                    ClearStatus();
                else
                {
                    routes = hexMap.GetCurrentRoutes();
                    currentTarget = 0;
                    Move();
                }
            }

        }
        else if(currentStatus == Status.IsMoving)
        {
            selectedPawn.transform.position = Vector3.Lerp(selectedPawn.transform.position, 
                routes[currentTarget].transform.position, Time.deltaTime * moveSpeed);
            float distance = Vector3.Distance(selectedPawn.transform.position, routes[currentTarget].transform.position);
            if(distance < 0.1f)
            {
                hexMap.RevealCellsFrom(routes[currentTarget]);
                if (currentTarget < routes.Count - 1)
                {
                    currentTarget++;
                }
                    
                else
                {
                    ClearStatus();
                }
            }
        }
        else if(currentStatus == Status.IsAttacking)
        {
            Debug.Log("Showing attack animation");
            ClearStatus();
        }
    }

    private void UpdateRoute()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.GetComponent<HexCell>())
            {
                HexCell toCell = hit.collider.GetComponent<HexCell>();
                if(!toCell.CanbeDestination() ||
                    toCell == selectedPawn.currentCell ||
                    !hexMap.IsReachable(toCell))
                {
                    validRoute = false;
                }
                else
                {
                    validRoute = true;
                }
                hexMap.FindPath(selectedPawn.currentCell, toCell);
                //hexMap.ShowPath(selectedPawn.currentCell, toCell);
            }
        }
    }

    public void UpdateAttackTarget()
    {
        HexCell cell = getCurrentPointerCell();
        if (cell != null && cell.CanbeAttackTargetOf(selectedPawn.currentCell))
        {
            validAttackTarget = true;
            attackTarget = cell.pawn;
        }
        else
        {
            Pawn pawn = getCurrentPointerPawn();
            if(pawn.currentCell.CanbeAttackTargetOf(selectedPawn.currentCell))
            {
                validAttackTarget = true;
                attackTarget = pawn;
            }
            else
            {
                validAttackTarget = false;
            }
        }
    }

    private void ClearStatus()
    {
        hexMap.HideIndicator();
        currentStatus = Status.Rest;
        gameInteraction.IsPawnAction = false;
    }

    public HexCell getCurrentPointerCell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<HexCell>())
            {
                return hit.collider.GetComponent<HexCell>();
            }
        }
        return null;
    }

    public Pawn getCurrentPointerPawn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<Monster>())
            {
                return hit.collider.GetComponent<Monster>();
            }
            else if (hit.collider.GetComponent<Enemy>())
            {
                return hit.collider.GetComponent<Enemy>();
            }
        }
        return null;
    }

    public void UpdateRoot(Pawn pawn, HexCell oldCell, HexCell newCell)
    {
        pawn.currentCell = newCell;
        newCell.pawn = pawn;
        oldCell.pawn = null;
    }

}
