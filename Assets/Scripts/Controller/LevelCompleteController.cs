using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SunnyLand
{
    internal class LevelCompleteController : IDisposable
    {
        private Vector3 _startPosition;
        private PlayerView _playerView;
        private List<PlayerView> _deathZones;
        private List<PlayerView> _winZones;

        public LevelCompleteController(PlayerView playerView, List<PlayerView> deathZones, List<PlayerView> winzones)
        {
            _startPosition = playerView.transform.position;
            //_playerView.OnObjectContact += OnObjectContact;
            _playerView = playerView;
            _deathZones = deathZones;
            _winZones = winzones;

            foreach(var deathzone in deathZones)
            {
                //deathzone.OnObjectContact += OnObjectContact;
            }
        }

        private void OnObjectContact(PlayerView contactView)
        {
            if (_deathZones.Contains(contactView))
            {
                _playerView.transform.position = _startPosition;
                Debug.Log("Warning!");
            }
        }


        public void Dispose()
        {
            //_playerView.OnObjectContact -= OnObjectContact;
        }

    }
}
