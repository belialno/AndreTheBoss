using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInteraction : MonoBehaviour
{

    public HexMap hexMap;
    public GameCamera gameCamera;
    public Canvas canvas;

    private Pawn selectedPawn;
    private HexCell currentCell;
    public PawnAction pawnActionPanel;
    public PawnStatus pawnStatusPanel;

    public bool IsPawnAction = false;

    public void OnEnable()
    {
        DisableAllPanels();
    }

    public void Update()
    {
        if (IsPawnAction)
            return;
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            UpdateSelect();
        }
        else if(Input.GetMouseButtonDown(2) && !EventSystem.current.IsPointerOverGameObject())
        {
            UpdateFocus();
        }
    }

    private void UpdateSelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            DisableAllPanels();
            DisableIndicators();
            if (hit.collider.GetComponent<HexCell>() != null)
            {
                hexMap.SelectHex(hit.point);
            }
            else if ((selectedPawn = hit.collider.GetComponent<Pawn>()) != null)
            {
                currentCell = hexMap.GetCellFromPosition(hit.point);
                selectedPawn.currentCell = currentCell;
                pawnActionPanel.SetPawn(selectedPawn);
                pawnStatusPanel.UpdatePawnStatusPanel(selectedPawn);
                EnableAllPawnPanels();
                hexMap.UnselectHex();
            }
        }
        else
        {
            ClearScreen();
        }
    }

    private void EnableAllPawnPanels()
    {
        pawnActionPanel.gameObject.SetActive(true);
        pawnStatusPanel.gameObject.SetActive(true);
    }

    private void DisableAllPanels()
    {
        pawnActionPanel.gameObject.SetActive(false);
        pawnStatusPanel.gameObject.SetActive(false);
    }

    private void DisableIndicators()
    {
        hexMap.HideIndicator();
    }

    private void UpdateFocus()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<HexCell>() != null)
            {
                gameCamera.FocusOnPoint(hit.point);
            }
        }
    }

    private void ClearScreen()
    {
        DisableIndicators();
        DisableAllPanels();
    }

}


