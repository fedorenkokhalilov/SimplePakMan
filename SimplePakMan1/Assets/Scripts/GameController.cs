using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	[SerializeField]
	private CellComponent _prefab;
	[SerializeField]
	private Transform _wrapper;

	private int _rowCount=10;
	private int _columnCount=10;
	private int _prevSelRow;
	private int _prevSelColumn;

	private CellComponent[,] _field;


	void Start () {
		GenerateField ();
	}

	private void LateUpdate()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			_field [_prevSelRow, _prevSelColumn].ISSelected = false;
			_prevSelRow--;
			_prevSelRow=_prevSelRow<0 ?_field.GetLength(0)-1 : _prevSelRow;
			_field [_prevSelRow, _prevSelColumn].ISSelected = true;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			_field [_prevSelRow, _prevSelColumn].ISSelected = false;

			_prevSelRow++;
			_prevSelRow=_prevSelRow>= _field.GetLength(0)?0:_prevSelRow;
			_field [_prevSelRow, _prevSelColumn].ISSelected = true;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			_field [_prevSelRow, _prevSelColumn].ISSelected = false;
			_prevSelColumn++;
			_prevSelColumn=_prevSelColumn>=_field.GetLength(1)-1? 0 : _prevSelColumn;
			_field [_prevSelRow, _prevSelColumn].ISSelected = true;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			_field [_prevSelRow, _prevSelColumn].ISSelected = false;

			_prevSelColumn--;
			_prevSelColumn=_prevSelColumn <0 ?_field.GetLength(1)-1:_prevSelColumn;
			_field [_prevSelRow, _prevSelColumn].ISSelected = true;
		}
	}

	void GenerateField () {
		if (!_prefab || !_wrapper)
			return;
		_field = new CellComponent[_rowCount, _columnCount];
		for (int i = 0; i < _rowCount; i++) {
			for (int j = 0; j < _columnCount; j++) {
				var item =  _prefab.GetInstance();
				item.transform.SetParent (_wrapper, false);
				_field [i, j] = item;
				item.gameObject.name = string.Format ("{0},{1}", i, j);

				item.Row = i;
				item.Column = j;
				item.OnItemClic+= Item_OnItemClic;
			}
		}
		}

	void Item_OnItemClic (int row, int column)
	{
		Debug.LogFormat ("Row {0}, Column {1}", row, column);
		_field [_prevSelRow, _prevSelColumn].ISSelected = false;
		_field [row, column].ISSelected = true;
		_prevSelRow = row;
		_prevSelColumn = column;
	}
	}
