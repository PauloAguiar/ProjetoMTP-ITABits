
namespace Projeto_Apollo_16
{
    public partial class GamePlayScreen : BaseGameState
    {
        CameraClass camera;
        
        private void cameraUpdate()
        {
            sectorLabel.Text = "Zoom:" + player.Zoom;
            positionLabel.Text = "Position:" + (int)player.GlobalPosition.X + " " + (int)player.GlobalPosition.Y;
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            weaponLabel.Text = "Weapon:" + player.bullets;
            timeLabel.Text = "Time: " + ((int)GameLogic.timeCreateEnemies).ToString();

            //statusLabel.Text = NetworkClass.status;
            if (systemRef.NETWORK_MODE)
                statusLabel.Text = systemRef.networkManager.status;

            camera.Zoom = player.Zoom;
            camera.Position = player.GlobalPosition;
            camera.Offset = player.CameraPosition;
        }

    }
}
