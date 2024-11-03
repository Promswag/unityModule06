using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
	[SerializeField] private Image _image;
	[SerializeField] private GameObject _mesh;
	private Color _defaultColor;
	private Color _newColor;
	private AudioSource _audioSource;
	private bool _isLooted;

	void Start()
	{
		_isLooted = false;
		_audioSource = GetComponent<AudioSource>();
		_defaultColor = _image.color;
		_newColor = new (_defaultColor.r, _defaultColor.g, _defaultColor.b, 1f);
		GameManager.Instance._reset += ResetState;
	}

	void Update()
	{
		if (!_isLooted)
		{
			if (Physics.CheckSphere(transform.position, 0.2f, 1 << LayerMask.NameToLayer("Player")))
			{
				_isLooted = true;
				_audioSource.Play();
				_mesh.SetActive(false);
				_image.color = _newColor;
				GameManager.Instance.AddKeyToInventory();
			}
		}
	}

	void ResetState()
	{
		_mesh.SetActive(true);
		_image.color = _defaultColor;
		_isLooted = false;
	}
}
