using System;
using UnityEngine;

namespace Example.Scripts.Homework
{
    public class HwCoordinate : MonoBehaviour
    {
        [SerializeField] private Transform _man;
        [SerializeField] private Transform[] _chicks;

        [SerializeField] private GameObject _bezierObj;

        private int _currentChick;

        private void Start()
        {
            _currentChick = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                RotateToNextChick(RotateToChickWithId);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                RotateToNextChick(RotateToChickWithIdByAngle);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                SpawnBezierObject();
            }
        }

        private void RotateToNextChick(Action<int> method)
        {
            method?.Invoke((_currentChick + 1) % _chicks.Length);
            _currentChick++;
        }
        
        private void RotateToChickWithId(int id)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back, _chicks[id].position - _man.position);           
        }

        private void RotateToChickWithIdByAngle(int id)
        {
            var angleWithOy = Vector3.SignedAngle(_chicks[id].position - _man.position, Vector3.up, Vector3.back);

            transform.up = _chicks[id].position - _man.position;
        }

        private void SpawnBezierObject()
        {
            Instantiate(_bezierObj, transform);
        }
    }
}