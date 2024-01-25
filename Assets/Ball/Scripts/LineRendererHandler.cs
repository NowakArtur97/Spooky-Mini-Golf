using UnityEngine;

public class LineRendererHandler : MonoBehaviour
{
    private LineRenderer _myLineRenderer;

    private void Awake() => _myLineRenderer = gameObject.GetComponent<LineRenderer>();

    public void StartRendering() => _myLineRenderer.positionCount = 2;

    public void Render(Vector2 direction, Vector3 position, float length, float maxLength)
    {
        _myLineRenderer.SetPosition(0, transform.position);
        _myLineRenderer.SetPosition(1, (Vector2)transform.position +
            Vector2.ClampMagnitude((direction * length) / 2, maxLength / 2));
    }

    public void StopRendering() => _myLineRenderer.positionCount = 0;
}
