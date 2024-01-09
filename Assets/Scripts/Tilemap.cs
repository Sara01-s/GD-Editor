using UnityEngine;

public sealed class Tilemap {

	private readonly Grid<TilemapObject> _tilemapGrid;

	public Tilemap(int width, int height, float cellSize, Vector3 originPosition) {
		_tilemapGrid = new(width, height, cellSize, originPosition, 
						   (Grid<TilemapObject> g, int x, int y) => new TilemapObject(g, x, y));
	}

	public void SetTilemapSprite(Vector3 worldPosition, TilemapObject.TilemapSprite tilemapSprite) {
		var tilemapObject = _tilemapGrid.GetCellGridObject(worldPosition);
		tilemapObject?.SetTilemapSprite(tilemapSprite);
	}

	public class TilemapObject {

		public enum TilemapSprite {
			None,
			Ground
		}

		private readonly Grid<TilemapObject> _tilemapGrid;
		private TilemapSprite _tilemapSprite;
		private readonly int _x, _y;

		public TilemapObject(Grid<TilemapObject> tilemapGrid, int x, int y) {
			_tilemapGrid = tilemapGrid;
			_x = x;
			_y = y;
		}

		public void SetTilemapSprite(TilemapSprite tilemapSprite) {
			_tilemapSprite = tilemapSprite;
			_tilemapGrid.RaiseGridObjectChanged(_x, _y);
		}

		public override string ToString() {
			return _tilemapSprite.ToString();
		}

	}


    
}
