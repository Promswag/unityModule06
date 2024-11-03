using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    
    [SerializeField] private int _keysCount;
    private int _keysInInventory;
    public event Action _reset;

    [SerializeField] private Animator _fadeAnimator;
    [SerializeField] private GameObject _caughtPanel;
    [SerializeField] private GameObject _wonPanel;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            _keysInInventory = 0;
        }
    }

    public void AddKeyToInventory()
    {
        _keysInInventory++;
    }

    void ResetState()
    {
        _keysInInventory = 0;
        _reset?.Invoke();
    }

    public bool HasEnoughKeys()
    {
        return _keysInInventory == _keysCount;
    }

    public void Caught()
    {
        _caughtPanel.SetActive(true);
        _fadeAnimator.SetTrigger("FadeIn");
        ResetState();
    }

    public void ResetFadeState()
    {
        _caughtPanel.SetActive(false);
        // _caughtPanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
    }

    void Victory()
    {
        _wonPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
