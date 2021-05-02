using System.Collections;
using UnityEngine;



public class IdleGoblinModel : BaseGoblinModel
{
    private Transform[] _strifePoints = null;
    private Transform _currentPoint;
    private Transform _nextPoint;
    private int _nextPointIndex = 0;
    private float _currentSmoothValue;
    private float _smoothSpeed = 0.005f;

    private float _lastAttackTime = 0;
    private float _currentTime;
    public override void Execute(GoblinView view)
    {
        base.Execute(view);
        if (_strifePoints == null)
        {
            _currentSmoothValue = 0;
            _strifePoints = view.StrifePoints;
            UpdateCurrentStrifePoints(0);
        }
        _currentTime = Time.time;
        _currentSmoothValue += _smoothSpeed;
        view.transform.position = Vector3.Lerp(_currentPoint.position, _nextPoint.position, _currentSmoothValue);

        if (_currentSmoothValue == 1)
        {
            UpdateCurrentStrifePoints(_nextPointIndex);
            _currentSmoothValue = 0;
        }

        if (_currentTime - _lastAttackTime > view.AttackCooldown)
        {
            view.MainAnimator.SetTrigger("Attack");
        }

    }

    private void UpdateCurrentStrifePoints(int index)
    {
        _currentPoint = _strifePoints[index];
        _nextPointIndex = index + 1;
        if (_nextPointIndex >= _strifePoints.Length)
        { 
            _nextPointIndex = 0;
        }
        _nextPoint = _strifePoints[_nextPointIndex];
    }
}
