namespace HyperCell.Test.Jelly {
    public interface ICameraMover {
        void SetMenuPosition();
        void MoveToWinPosition();
        void MoveToGamePosition();
        void MoveCamera(float timeDelta);
    }
}