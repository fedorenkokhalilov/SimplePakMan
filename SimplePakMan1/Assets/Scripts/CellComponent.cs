using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;

public class CellComponent : MonoBehaviour {

	public event Action<int, int> OnItemClic;

	private void OnItemClicHandler()
	{
		if (OnItemClic != null)
			OnItemClic (Row, Column);
	}

	private float _delay = 0.25f;

			public int Row {
				get;
				set;
			}

			public int Column {
				get;
				set;
			}

	[SerializeField]
	private Image _image;

	[SerializeField]
	private Color _selectedColor;

	private Color _normalColor;


	public void OnSelect(){
		OnItemClicHandler ();	}

	bool _isSelected;
	public bool ISSelected{
		set{ 
			_isSelected = value;
			CancelInvoke ();
			if (value)
				InvokeRepeating ("SwitchColor", 0, _delay);
			else if (_image) {
				_image.color = _normalColor;
			}
		}
		get{ 
			return _isSelected;
		}
	}



	// Use this for initialization
	void Start () {
		_normalColor = _image.color;
	}
	
	// Update is called once per frame


	private void SwitchColor()
	{
		if (!_image)
			return;
		_image.color = _image.color== _normalColor ? _selectedColor : _normalColor;
	}
}
