using SweetEngine.MDI.Ingredients;
using SweetLib.Intents.Generated;
using SweetLib.Intents;
using SweetLib.Devices;
using System.Numerics;

namespace SweetEngine.MDI.Mixers;

public unsafe struct CameraMixer
{
    public Transform Transform;

    private readonly float speedMultiplier;
    private readonly float sensitivity;
    private readonly float speed;

    public float Aspect;

    public CameraMixer()
    {
        Transform = new Transform()
        {
            Position = new(0, 0, 20),
            Rotation = Vector3.Zero
        };

        speedMultiplier = 1.01f;
        sensitivity = 0.002f;
        speed = 25f;
    }

    public void Whip(in Intent intent, in Time time, in Mouse mouse)
    {
        if (intent.IsHeld(EditorCameraIntents.MoveState))
        {
            Movement(in intent, in time);
            Rotating(in mouse);
        }
    }

    private void Movement(in Intent intent, in Time time)
    {
        Vector3 direction =
            Transform.GetForward() * intent.GetAxis(EditorCameraIntents.MoveForward) +
            Transform.GetRight() * intent.GetAxis(EditorCameraIntents.MoveRight) +
            Transform.GetUp() * intent.GetAxis(EditorCameraIntents.MoveUp);

        float currentSpeed = intent.IsHeld(EditorCameraIntents.Sprint) ?
            speed * speedMultiplier :
            speed;

        if (direction != Vector3.Zero)
            direction = Vector3.Normalize(direction);

        Transform.Position += direction * currentSpeed * time.Delta;
    }

    private void Rotating(in Mouse mouse)
    {
        Transform.Rotation.Y += sensitivity * mouse.Delta.X;
        Transform.Rotation.X += sensitivity * mouse.Delta.Y;

        Transform.Rotation.X = Math.Clamp(
            Transform.Rotation.X,
            -MathF.PI / 2 + 0.01f,
            MathF.PI / 2 - 0.01f);
    }
}