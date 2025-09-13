using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private House _house;

    private AudioSource _audioSource;
    private float _timeToChangedVolume = 2f;
    private float _timePassed;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;

    public event Action alarmEnabled;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
        _audioSource.playOnAwake = true;
    }

    private void OnEnable()
    {
        _house.housebreakingDetekted += AlarmOn;
        _house.housebreakingUndetekted += AlarmOff;
    }

    private void OnDisable()
    {
        _house.housebreakingDetekted -= AlarmOn;
        _house.housebreakingUndetekted -= AlarmOff;
    }

    private IEnumerator IncreasingVolume()
    {
        _timePassed = 0;

        while (_audioSource.volume <= _maxVolume)
        {
            _timePassed += Time.deltaTime;

            _audioSource.volume = Mathf.MoveTowards(_minVolume, _maxVolume, _timePassed / _timeToChangedVolume);

            yield return null;
        }
    }

    private IEnumerator ReductingVolume()
    {
        _timePassed = 0;

        while (_audioSource.volume >= _minVolume)
        {
            _timePassed += Time.deltaTime;

            _audioSource.volume = Mathf.MoveTowards(_maxVolume, _minVolume, _timePassed / _timeToChangedVolume);

            yield return null;
        }
    }

    private void AlarmOn()
    {
        alarmEnabled?.Invoke();

        StopAllCoroutines();
        StartCoroutine(IncreasingVolume());
    }

    private void AlarmOff()
    {
        StopAllCoroutines();
        StartCoroutine(ReductingVolume());
    }
}
