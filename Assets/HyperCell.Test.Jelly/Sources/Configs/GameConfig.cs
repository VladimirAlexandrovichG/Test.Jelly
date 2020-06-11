namespace HyperCell.Test.Jelly {
    using System;
    using UnityEngine;
    using System.Collections.Generic;

    [CreateAssetMenu(fileName = "GameConfig", menuName = "Jelly/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject {
        public Jelly jellyPrefab;
        public Vector3 jellyInitPosition;

        [Space] 
        public List<GameObject> levels;
        public Vector3 levelInitPosition;
        public int startLevel;
        public string levelPrefsKey;
        
        [Space]
        public MoveConfig moveConfig;
        
        [Space]
        public ObstacleBounceConfig obstacleBounceConfig;

        [Space] 
        public CollisionObstacleOfJellyConfig collisionObstacleOfJellyConfig;
        
        [Space]
        public TargetObstacleProjectionConfig projectionConfig;
        
        [Space]
        public ResizerConfig resizerConfig;

        [Space] 
        public CameraMoveConfig cameraMoveConfig;
    }

    [Serializable]
    public struct MoveConfig {
        public float maxSpeed;
        public float moveForce;
    }

    [Serializable]
    public struct ObstacleBounceConfig {
        public float bounceDistance;
        public float bouncePower;
        public float bounceStateDuration;
    }

    [Serializable]
    public struct TargetObstacleProjectionConfig {
        public float maxDistance;
        public LayerMask obstacleViewMask;
    }

    [Serializable]
    public struct ResizerConfig {
        public float minScale;
        public float maxScale;
    }

    [Serializable]
    public struct CameraMoveConfig {
        public Vector3 menuStatePosition;
        public Vector3 menuStateRotation;
        
        [Space]
        public Vector3 gameStatePosition;
        public Vector3 gameStateRotation;
        
        [Space]
        public Vector3 winStatePosition;
        public Vector3 winStateRotation;
        
        [Space]
        public float moveToGameStateDuration;
        public float moveToWinStateDuration;
        public float distance;
    }

    [Serializable]
    public struct CollisionObstacleOfJellyConfig {
        public float obstacleMass;
        public float forceFrom;
        public float forceTo;
    }
}