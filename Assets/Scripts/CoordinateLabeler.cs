using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [Header("Text colors above the tiles")]
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockkedColor = Color.grey;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f,0.5f,0f);


    TextMeshPro Label;
    Vector2Int coordinates = new Vector2Int();

    GridManager gridManager;


    private void Awake()
    {
        Label = GetComponent<TextMeshPro>();
        Label.enabled = false;


        gridManager = FindObjectOfType<GridManager>();
        DisplayCoordinates();
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            Label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
        
    }

    private void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Label.enabled = !Label.IsActive();
        }
    }

    private void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);
        if(node == null) { return; }

        if(!node.isWalkable)
        {
            Label.color = blockkedColor;
        }
        else if ( node.isPath)
        {
            Label.color = pathColor;
        }
        else if( node.isExplored)
        {
            Label.color = exploredColor;
        }
        else
        {
            Label.color = defaultColor;
        }
    }

    void DisplayCoordinates()
    {
        if(gridManager == null) { return; }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        Label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
