using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BoostSystem : MonoBehaviour
{
    [SerializeField] private Slider _BoostBarSlider;
    [SerializeField] private float _boostDepletionRate;
    [SerializeField] private float _boostFillRate;
    [SerializeField] private float _boostSpeed;

    private float _boostRemaining;
    private PlayerController _player;
    private bool _isBoosting;

    private void Awake()
    {
        _player = gameObject.GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        // the underscore here denotes you don't need to pass anything into the Offence method
        References.Controls.Player.Offence.started += _ => BoostStarted();
        References.Controls.Player.Offence.performed += _ => BoostEnded();

        // If you want to pass in a value, use it like this
        // "context" is just a temp variable that could actually be called anything
        // _controls.Player.Offence.performed += context => Offence(cxt.ReadValue<float>());

        _boostRemaining = _BoostBarSlider.maxValue;
        _BoostBarSlider.value = _boostRemaining;
    }

    private void Update()
    {
        CalculateBoost();
    }

    private void CalculateBoost()
    {
        // boosting and more than empty
        if (_isBoosting && _boostRemaining > _BoostBarSlider.minValue)
        {
            _BoostBarSlider.gameObject.SetActive(true);
            _boostRemaining -= Time.deltaTime * _boostDepletionRate;
            UpdateBoostBarUI(_boostRemaining);
        }

        // not boosting and less than full
        else if (!_isBoosting && _boostRemaining < _BoostBarSlider.maxValue)
        {
            _BoostBarSlider.gameObject.SetActive(true);
            _boostRemaining += Time.deltaTime * _boostFillRate;
            UpdateBoostBarUI(_boostRemaining);
        }

        // not boosting and full
        if (_boostRemaining >= _BoostBarSlider.maxValue)
        {
            _BoostBarSlider.gameObject.SetActive(false);
        }
    }

    private void UpdateBoostBarUI(float boost)
    {
        _BoostBarSlider.value = boost;
    }

    public void BoostStarted()
    {
        if (_boostDepletionRate <= 0) return;

        _isBoosting = true;
        _player.CurrentMoveSpeed = _boostSpeed;
    }

    public void BoostEnded()
    {
        _isBoosting = false;
        _player.CurrentMoveSpeed = _player.BaseMoveSpeedMultiplier;
        // Debug.Log($"isDashing = {_isBoosting}");
    }
}