using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private AudioSource _audioSource;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;

    public event Action EnabledSiren;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
        _audioSource.playOnAwake = true;
    }

    private IEnumerator ChangingVolume(float targetVolume)
    {
        float startingVolume = _audioSource.volume;
        float volumeChangedStep = 0.1f;

        while (!Mathf.Approximately(_audioSource.volume, targetVolume))
        {
            _audioSource.volume = Mathf.MoveTowards(startingVolume, targetVolume, volumeChangedStep += Time.deltaTime);

            yield return null;
        }
    }

    public void EnableSiren()
    {
        EnabledSiren?.Invoke();

        StopAllCoroutines();
        StartCoroutine(ChangingVolume(_maxVolume));
    }

    public void DisableSiren()
    {
        StopAllCoroutines();
        StartCoroutine(ChangingVolume(_minVolume));
    }
}