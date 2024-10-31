using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
	[SerializeField] private Image _image;
	private Color _defaultColor;
	private Color _newColor;

	void Start()
	{
		_defaultColor = _image.color;
		_newColor = new (_defaultColor.r, _defaultColor.g, _defaultColor.b, 1f);
	}

	void Update()
	{
		Debug.Log("?");
		if (Physics.CheckSphere(transform.position, 1f, 1 << LayerMask.NameToLayer("Player")))
		{
			Debug.Log("XD");
			_image.color = _newColor;
			enabled = false;
			gameObject.SetActive(false);
		}
	}

	void Reset()
	{
		_image.color = _defaultColor;
		enabled = true;
	}
}
