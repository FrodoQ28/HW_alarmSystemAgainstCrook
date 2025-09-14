using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private SecuritySensor _detector;
    [SerializeField] private Alarm _alarm;

    private void OnEnable()
    {
        _detector.HousebreakingDetekted += AlarmOn;
        _detector.HousebreakingUndetekted += AlarmOff;
    }
    private void OnDisable()
    {
        _detector.HousebreakingDetekted -= AlarmOn;
        _detector.HousebreakingUndetekted -= AlarmOff;
    }

    private void AlarmOn() =>
        _alarm.EnableSiren();

    private void AlarmOff() =>
        _alarm.DisableSiren();
}