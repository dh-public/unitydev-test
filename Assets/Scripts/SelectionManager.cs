using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Color selectedColor;

    private int emissionID;
    private Transform lastSelected;
    private Transform selected;

    private void Awake() => emissionID = Shader.PropertyToID("_EmissionColor");

    private void Update()
    {
        if (!selected)
        {
            DisableLastSelected();
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            if (hit.transform.CompareTag(selectableTag))
            {
                DisableLastSelected();
                lastSelected = selected = hit.transform;
            }
            else
                selected = null;
        }
        else
            selected = null;

        if (selected)
        {
            var selectedRenderer = selected.GetComponent<Renderer>();
            selectedRenderer.material.EnableKeyword("_EMISSION");
            selectedRenderer.material.SetColor(emissionID, selectedColor);
        }
    }

    private void DisableLastSelected()
    {
        lastSelected?.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }
}
