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

    public event Action AlarmEnabled;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
        _audioSource.playOnAwake = true;
    }

    private void OnEnable()
    {
        _house.HousebreakingDetekted += AlarmOn;
        _house.HousebreakingUndetekted += AlarmOff;
    }

    private void OnDisable()
    {
        _house.HousebreakingDetekted -= AlarmOn;
        _house.HousebreakingUndetekted -= AlarmOff;
    }

    private IEnumerator ChangingVolume(float targetVolume)
    {
        float startingVolume = _audioSource.volume;

        _timePassed = 0f;

        while (!Mathf.Approximately(_audioSource.volume, targetVolume))
        {
            _timePassed += Time.deltaTime;

            _audioSource.volume = Mathf.MoveTowards(startingVolume, targetVolume, _timePassed / _timeToChangedVolume);

            yield return null;
        }
    }

    private void AlarmOn()
    {
        AlarmEnabled?.Invoke();

        StopAllCoroutines();
        StartCoroutine(ChangingVolume(_maxVolume));
    }

    private void AlarmOff()
    {
        StopAllCoroutines();
        StartCoroutine(ChangingVolume(_minVolume));
    }
}
