using Microsoft.Xna.Framework;

namespace Application.Interface.Camera;

public interface ICameraService
{
    public Matrix Transform { get; }

    void Follow(Vector2 target);
}
