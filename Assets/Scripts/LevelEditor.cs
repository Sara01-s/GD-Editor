using UnityEngine;

public sealed class LevelEditor : MonoBehaviour {

	private Grid<int> _levelEditorGrid;
    
	private void Awake() {
		_levelEditorGrid = new(8, 4, 2.0f, transform.position, (Grid<int> g, int x, int y) => 0);
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			_levelEditorGrid.SetCellGridObject(Utils.GetMouseWorldPosition(), 57);
		}

		if (Input.GetMouseButtonDown(1)) {
			print(_levelEditorGrid.GetCellGridObject(Utils.GetMouseWorldPosition()));
		}
	}
}
