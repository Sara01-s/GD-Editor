using UnityEngine;

public static class Utils {

	public static Vector3 GetMouseWorldPosition() {
		var mousePosition = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
		mousePosition.z = 0.0f;

		return mousePosition;
	}

	public static Vector3 GetMouseWorldPositionWithZ() {
		return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
	}

	public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) {
		return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
	}

	public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
		return worldCamera.ScreenToWorldPoint(screenPosition);
	}

	public static TextMesh CreateWorldText(TextMeshData textMeshData) {
		if (textMeshData.Color == default) {
			textMeshData.Color = Color.white;
		}

		return CreateWorldTextInternal(new (textMeshData.Text, textMeshData.LocalPosition, textMeshData.Parent, textMeshData.FontSize,
							   		   textMeshData.Anchor, textMeshData.Alignment, textMeshData.Color, textMeshData.SortingOrder));
	}
    
	private static TextMesh CreateWorldTextInternal(TextMeshData textMeshData) {
		var gameObject = new GameObject("World Text", typeof(TextMesh));
		var transform = gameObject.transform;

		transform.SetParent(textMeshData.Parent, worldPositionStays: false);
		transform.localPosition = textMeshData.LocalPosition;

		var textMesh = gameObject.GetComponent<TextMesh>();

		textMesh.anchor = textMeshData.Anchor;
		textMesh.alignment = textMeshData.Alignment;
		textMesh.text = textMeshData.Text;
		textMesh.fontSize = textMeshData.FontSize;
		textMesh.color = textMeshData.Color;
		textMesh.GetComponent<MeshRenderer>().sortingOrder = textMeshData.SortingOrder;

		return textMesh;
	}
	
}

public struct TextMeshData {
	public string Text;
	public Vector3 LocalPosition;
	public int FontSize;
	public Transform Parent;
	public TextAnchor Anchor;
	public TextAlignment Alignment;
	public Color Color;
	public int SortingOrder;

	public TextMeshData(string text, Vector3 localPosition, Transform parent, int fontSize = 40 , TextAnchor anchor = TextAnchor.MiddleCenter, TextAlignment alignment = TextAlignment.Center, Color color = default, int sortingOrder = 0) {
		Text = text;
		LocalPosition = localPosition;
		Parent = parent;
		FontSize = fontSize;
		Anchor = anchor;
		Alignment = alignment;
		Color = color;
		SortingOrder = sortingOrder;
	}
}