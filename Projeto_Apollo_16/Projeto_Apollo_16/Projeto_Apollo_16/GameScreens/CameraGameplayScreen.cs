
namespace Projeto_Apollo_16
{
    public partial class GamePlayScreen : BaseGameState
    {
        CameraClass camera;
        
        private void CameraUpdate()
        {
            UpdateLabels();

            camera.Zoom = player.Zoom;
            camera.Position = player.GlobalPosition;
            camera.Offset = player.CameraPosition;
        }

    }
}
