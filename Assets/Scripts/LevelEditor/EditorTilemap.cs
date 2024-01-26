using UnityEngine;

public sealed class EditorTilemap : MonoBehaviour {

	[SerializeField] private int _numRows;
	[SerializeField] private int _numColumns;
	[SerializeField, Min(0.0f)] private float _cellSize;

	private Tilemap _editorTilemap;

	private void Awake() {
		_editorTilemap = new(_numRows, _numColumns, _cellSize, transform.position);
	}

	private void Update() {
		if (Input.GetMouseButton(0)) {
			var mousePosition = Utils.GetMouseWorldPosition();

			_editorTilemap.SetTilemapSprite(mousePosition, Tilemap.TilemapObject.TilemapSprite.Orb);
		}
	}
}