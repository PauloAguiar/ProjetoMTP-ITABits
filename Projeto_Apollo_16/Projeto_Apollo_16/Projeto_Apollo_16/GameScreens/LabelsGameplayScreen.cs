using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Projeto_Apollo_16
{
    public partial class GamePlayScreen : BaseGameState
    {
        #region labels
        Label sectorLabel;
        Label positionLabel;
        Label cameraLabel;
        Label statusLabel;
        Label weaponLabel;
        Label timeLabel;
        Label itemLabel;
        #endregion

        private void LoadLabels()
        {
            sectorLabel = new Label();
            sectorLabel.Position = Vector2.Zero;
            sectorLabel.Text = "Zoom:" + player.Zoom;
            sectorLabel.Color = Color.Blue;
            sectorLabel.Size = sectorLabel.SpriteFont.MeasureString(sectorLabel.Text);
            controlManager.Add(sectorLabel);

            positionLabel = new Label();
            positionLabel.Position = Vector2.Zero + 1 * (new Vector2(0.0f, 25.0f));
            positionLabel.Text = "Position:" + player.GlobalPosition.X + " " + player.GlobalPosition.Y;
            positionLabel.Color = Color.Yellow;
            positionLabel.Size = positionLabel.SpriteFont.MeasureString(positionLabel.Text);
            controlManager.Add(positionLabel);

            cameraLabel = new Label();
            cameraLabel.Position = Vector2.Zero + 2 * (new Vector2(0.0f, 25.0f));
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            cameraLabel.Color = Color.Green;
            cameraLabel.Size = cameraLabel.SpriteFont.MeasureString(cameraLabel.Text);
            controlManager.Add(cameraLabel);

            statusLabel = new Label();
            statusLabel.Position = Vector2.Zero + 3 * (new Vector2(0.0f, 25.0f));
            statusLabel.Text = "Mode: Offline";
            statusLabel.Color = Color.Red;
            statusLabel.Size = statusLabel.SpriteFont.MeasureString(statusLabel.Text);
            controlManager.Add(statusLabel);

            weaponLabel = new Label();
            weaponLabel.Position = Vector2.Zero + 4 * (new Vector2(0.0f, 25.0f));
            weaponLabel.Text = "Weapon:" + player.bullets;
            weaponLabel.Color = Color.Purple;
            weaponLabel.Size = weaponLabel.SpriteFont.MeasureString(weaponLabel.Text);
            controlManager.Add(weaponLabel);

            timeLabel = new Label();
            timeLabel.Position = Vector2.Zero + 5 * (new Vector2(0.0f, 25.0f));
            timeLabel.Text = "Time: " + ((int)GameLogic.timeCreateEnemies).ToString();
            timeLabel.Color = Color.White;
            timeLabel.Size = timeLabel.SpriteFont.MeasureString(timeLabel.Text);
            controlManager.Add(timeLabel);

            itemLabel = new Label();
            itemLabel.Position = Vector2.Zero + 6 * (new Vector2(0.0f, 25.0f));
            itemLabel.Text = itemLabel.Text = "Item: " + (PlayerClass.items).ToString() + " " + player.inventory[(int)PlayerClass.items].ToString();
            itemLabel.Color = Color.Orange;
            itemLabel.Size = itemLabel.SpriteFont.MeasureString(itemLabel.Text);
            controlManager.Add(itemLabel);

        }

        private void UpdateLabels()
        {
            sectorLabel.Text = "Zoom:" + player.Zoom;
            positionLabel.Text = "Position:" + (int)player.GlobalPosition.X + " " + (int)player.GlobalPosition.Y;
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            weaponLabel.Text = "Weapon:" + player.bullets;
            timeLabel.Text = "Time: " + ((int)GameLogic.timeCreateEnemies).ToString();
            itemLabel.Text = "Item: " + (PlayerClass.items).ToString() + " " + player.inventory[(int)PlayerClass.items].ToString();

            if(systemRef.networkManager.GetStatus() == NetPeerStatus.Running)
                statusLabel.Text = "Modo: Online";
            else
                statusLabel.Text = "Modo: Offline";

        }
    }
}
