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


    TextMeshPro Label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    private void Awake()
    {
        Label = GetComponent<TextMeshPro>();
        Label.enabled = false;

        waypoint = GetComponentInParent<Waypoint>();
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
        if (waypoint.IsPlaceable == true)
            Label.color = defaultColor;
        else
            Label.color = blockkedColor;
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        Label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
