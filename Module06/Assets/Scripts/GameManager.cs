using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    
    [SerializeField] private int _keysCount;
    private int _keysInInventory;
    public event Action _reset;

    private AudioSource _audioSource;
    [SerializeField] private Animator _winAnimator;
    [SerializeField] private Animator _loseAnimator;
    [SerializeField] private GameObject _caughtPanel;
    [SerializeField] private GameObject _wonPanel;
    [SerializeField] private AudioClip _winAudioClip;
    [SerializeField] private AudioClip _loseAudioClip;

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

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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

    public void Win()
    {
        _wonPanel.SetActive(true);
        _winAnimator.SetTrigger("FadeIn");
        _audioSource.clip = _winAudioClip;
        _audioSource.Play();
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
    }

    public void Caught()
    {
        _caughtPanel.SetActive(true);
        _loseAnimator.SetTrigger("FadeIn");
        _audioSource.clip = _loseAudioClip;
        _audioSource.Play();
        ResetState();
    }

    public void ResetFadeState()
    {
        _caughtPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Win();
        }
    }
}
