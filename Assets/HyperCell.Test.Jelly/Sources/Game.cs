namespace HyperCell.Test.Jelly {
    using UnityEngine;
    using System;
    using System.Threading.Tasks;
    using UnityEngine.UI;
    using DG.Tweening;
    using UnityEngine.SceneManagement;
    
    public enum GameState {
        Menu = 1,
        Game = 2, 
        Win = 3, 
        WinMenu = 4,
        Lose = 5, 
        LoseMenu
    }

    public class Game : MonoBehaviour {
        public GameObject gameCamera;
        public GameConfig gameConfig;

        [Space]
        [Header("UI")]
        public CanvasGroup initUI;
        public CanvasGroup menuUI;
        public CanvasGroup winUI;
        public CanvasGroup loseUI;
        public Text loadingText;
        public Text levelText;

        private Jelly jelly;
        private GameState gameState = GameState.Menu;
        private int currentLevel;
        
        
        private IObjectMover objectMover;
        private IJellyResizer jellyResizer;
        private IJellyInputController jellyInputController;
        private ICameraMover cameraMover;
        private ITargetObstacleProjection targetObstacleProjection;
        private IWinEffects winEffects;

        private void Start() {
            Application.targetFrameRate = 60;

            this.jelly = Instantiate(
                this.gameConfig.jellyPrefab, 
                this.gameConfig.jellyInitPosition, 
                Quaternion.Euler(Vector3.zero));

            this.LoadCurrentLevelIndex();
            
            var level = Instantiate(
                this.gameConfig.levels[this.currentLevel], 
                this.gameConfig.levelInitPosition, 
                Quaternion.Euler(Vector3.zero));

            this.jellyResizer = new JellyResizer(
                this.jelly.transform.localScale, 
                this.gameConfig.resizerConfig.maxScale,
                this.gameConfig.resizerConfig.minScale);

            this.jelly.collisionJellyOfObstacleReaction = new DoTweenBounceObstacleJellyOfCollisionReaction(
                this.jelly,
                this.gameConfig.obstacleBounceConfig.bounceStateDuration, 
                this.gameConfig.obstacleBounceConfig.bounceDistance, 
                this.gameConfig.obstacleBounceConfig.bouncePower);
            
            this.jelly.collisionObstacleOfJellyReaction = new CollisionObstacleOfJellyReaction(
                this.gameConfig.collisionObstacleOfJellyConfig.obstacleMass, 
                this.gameConfig.collisionObstacleOfJellyConfig.forceFrom, 
                this.gameConfig.collisionObstacleOfJellyConfig.forceTo);

            this.objectMover = new ForceObjectMover(
                this.jelly.gameObject.GetComponent<Rigidbody>(),
                this.gameConfig.moveConfig.maxSpeed, 
                this.gameConfig.moveConfig.moveForce);

            this.jellyInputController = new JellyInputController();
            
            this.cameraMover = new CameraMoveRelative(
                this.gameCamera.transform, 
                this.jelly.transform, 
                this.gameConfig.cameraMoveConfig);
            
            this.targetObstacleProjection = new TargetObstacleProjection(
                this.jelly.transform,
                this.jelly.targetPlane, 
                this.jelly.targetBox, 
                this.gameConfig.projectionConfig.maxDistance, 
                this.gameConfig.projectionConfig.obstacleViewMask);
            
            this.winEffects = new WinEffects(this.jelly.gameObject);

            this.Init();
        }

        private void Update() {
            switch (this.gameState)
            {
                case GameState.Menu:
                    break;
                case GameState.Game:
                    this.GameStateUpdate();
                    break;
                case GameState.Win:
                    this.Win();
                    break;
                case GameState.WinMenu:
                    break;
                case GameState.Lose:
                    this.Lose();
                    break;
                case GameState.LoseMenu:
                    break;
            }
        }

        private void GameStateUpdate() {
            var input = this.jellyInputController.GetInput();
            this.jelly.UpdateJelly(Time.deltaTime);

            if(this.jelly.state == JellyState.NormalState)
                this.objectMover.Move(Vector3.forward, Time.deltaTime);
            else if (this.jelly.state == JellyState.WinState)
                this.gameState = GameState.Win;
            else if (this.jelly.state == JellyState.LoseState)
                this.gameState = GameState.Lose;
            
            this.jellyResizer.Resize(this.jelly.transform, input, Time.deltaTime);
            this.cameraMover.MoveCamera(Time.deltaTime);
            this.targetObstacleProjection.UpdateProjection();
        }

        private void Win() {
            this.cameraMover.MoveToWinPosition();
            this.objectMover.ForceStop();
            this.winEffects.DoWinEffects();
            this.targetObstacleProjection.SetProjectionActive(false);

            this.SetActiveUI(this.winUI, true);
            this.gameState = GameState.WinMenu;
            this.IncreaseCurrentLevelIndex();
        }

        private void Lose() {
            this.SetActiveUI(this.loseUI, true);
            this.targetObstacleProjection.SetProjectionActive(false);
            this.gameState = GameState.LoseMenu;
        }
        
        // Called by TAP TO PLAY btn
        public void StartPlay() {
            if(this.gameState != GameState.Menu) return;
            this.SetActiveUI(this.menuUI, false);

            this.gameState = GameState.Game;
            
            this.cameraMover.MoveToGamePosition();
            this.targetObstacleProjection.SetProjectionActive(true);
        }

        // Called by CONTINUE btn
        public void Continue() {
            SceneManager.LoadScene(0);
        }

        private void LoadCurrentLevelIndex() {
            if (PlayerPrefs.HasKey(this.gameConfig.levelPrefsKey))
            {
                this.currentLevel = PlayerPrefs.GetInt(this.gameConfig.levelPrefsKey);
            }
            else
            {
                this.currentLevel = this.gameConfig.startLevel;
            }

            if (this.currentLevel >= this.gameConfig.levels.Count)
            {
                this.currentLevel = this.gameConfig.startLevel;
            }

            this.levelText.text = (this.currentLevel + 1).ToString();
        }

        private void IncreaseCurrentLevelIndex() {
            this.currentLevel++;
            PlayerPrefs.SetInt(this.gameConfig.levelPrefsKey, this.currentLevel);
        }
        
        private async void Init() {
            this.cameraMover.SetMenuPosition();
            this.targetObstacleProjection.SetProjectionActive(false);
            
            this.loadingText.text = this.loadingText.text + ".";
            await Task.Delay(TimeSpan.FromSeconds(0.25f));
            this.loadingText.text = this.loadingText.text + ".";
            await Task.Delay(TimeSpan.FromSeconds(0.25f));
            this.loadingText.text = this.loadingText.text + ".";
            await Task.Delay(TimeSpan.FromSeconds(0.25f));
            
            this.SetActiveUI(this.initUI, false);
            this.gameState = GameState.Menu;
        }

        private void SetActiveUI(CanvasGroup canvasGroup, bool active) {
            canvasGroup.DOFade(active ? 1 : 0, 0.5f);
            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;
        }
    }
}