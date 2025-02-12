﻿using System;
using System.Linq;
using Systems.Save;
using UI.Map;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace StarMap
{
    [CreateAssetMenu(fileName = "Context", menuName = "Map/Context", order = 0)]
    public class MapContext : ScriptableObject
    {
        [SerializeField] private SaveManager saveManager;
        public StarSystem StartingSystem;
        public MapData Data;
        private Camera[] _activeCameras;

        private MapView _view;
        public StarSystem CurrentSystem { get; private set; }

        private void Awake()
        {
            CurrentSystem = StartingSystem;
        }

        public event Action<StarSystem, StarSystem> OnCurrentSystemChange;

        public void ShowMapView()
        {
            // load map scene additively if not loaded
            if (_view == null)
            {
                _view = FindObjectOfType<MapView>();
                if (_view == null)
                {
                    Debug.Log("Loading map");
                    var status = SceneManager.LoadSceneAsync("Scenes/MapScene", LoadSceneMode.Additive);
                    status.completed += operation => ActivateMapView();
                    return;
                }
            }

            ActivateMapView();
        }

        public void HideMapView()
        {
            // Deactivate map cam
            if (_view == null || !_view.enabled) return;
            _view.enabled = false;

            foreach (var camera in _activeCameras)
            {
                if (camera != null)
                {
                    camera.enabled = true;
                }
            }

            _activeCameras = null;
        }

        public bool IsAccessible(StarSystem system)
        {
            return Data.SystemPairs.Any(systemPair =>
                (systemPair.System1 == system || systemPair.System2 == system) &&
                (systemPair.System1 == CurrentSystem || systemPair.System2 == CurrentSystem));
        }

        public void LoadSystem(StarSystem system)
        {
            // Check if is accessible from current system
            if (!IsAccessible(system))
            {
                Debug.Assert(IsAccessible(system),
                    $"Trying to jump to inaccessible system {system.SystemName} from {CurrentSystem.SystemName}");
                return;
            }

            GameContext.Instance.CurrentState = GameContext.GameState.BattleMode;
            saveManager.Save();
            SceneManager.LoadScene("Scenes/BattleScene");
            OnCurrentSystemChange?.Invoke(CurrentSystem, system);
            CurrentSystem = system;
            CurrentSystem.LoadSystem();
        }

        private void ActivateMapView()
        {
            if (_view == null) _view = FindObjectOfType<MapView>();
            if (_view.enabled) return;
            _activeCameras = Camera.allCameras.Where(c => c.enabled).ToArray();
            _view.enabled = true;
            foreach (var camera in _activeCameras)
            {
                if (camera != null)
                {
                    camera.enabled = false;
                }
            }
        }
    }
}