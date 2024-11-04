using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips;
    private float _timeWaited = 0;
    private float _timeToWait = 0;
    [SerializeField] [Range(0f, 10f)] private float _maxDelayBetweenPlay;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _timeWaited += Time.deltaTime;
        if (_timeWaited > _timeToWait)
        {
            _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Count)];
            _audioSource.Play();
            _timeWaited = 0;
            _timeToWait = _audioSource.clip.length + Random.Range(0f, _maxDelayBetweenPlay);
        }
    }
}
