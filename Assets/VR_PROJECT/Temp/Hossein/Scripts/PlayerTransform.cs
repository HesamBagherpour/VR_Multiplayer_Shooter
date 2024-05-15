using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerTransformData
{
    public List<PlayerTransform> PlayerTransforms;

    #region CTORs

    public PlayerTransformData()
    {
        PlayerTransforms = new();

        foreach (PlayerTransformType type in Enum.GetValues(typeof(PlayerTransformType)))
        {
            if (type == PlayerTransformType.None)
                continue;

            var trasnform = new PlayerTransform() { Transform = new CustomTransform(), Type = type };

            PlayerTransforms.Add(trasnform);
        }
    }  
    public PlayerTransformData(List<PlayerTransform> playerTransforms)
    {
        PlayerTransforms = playerTransforms;
    }

    #endregion
    
    public void Set(PlayerTransform playerTransform)
    {
        if (playerTransform is null)
        {
            Debug.LogError("Player Transform is Null pls check your input");
            return;
        }

        var transform = GetTransform(playerTransform.Type);
        
        if(transform is null)
            return;

        var index = PlayerTransforms.IndexOf(transform);
        PlayerTransforms[index] = playerTransform;
    }  
    
    public void Set(PlayerTransformType type, Transform transform)
    {
        if (transform is null)
        {
            Debug.LogError("Transform is Null pls check your input");
            return;
        }

        var playerTransform = GetTransform(type);
        
        if(playerTransform is null)
            return;

        var index = PlayerTransforms.IndexOf(playerTransform);

        PlayerTransforms[index].Transform.Set(transform);
    }

    public PlayerTransform Get(PlayerTransformType type)
    {
        return GetTransform(type);
    }

    private PlayerTransform GetTransform(PlayerTransformType type)
    {
        var transform = PlayerTransforms.FirstOrDefault(p => p.Type == type);

        if (transform is null)
        {
            Debug.LogError($"Player Transform with type {type} not found!!");
            return null;
        }

        return transform;
    }
}

[Serializable]
public class PlayerTransform
{
    public PlayerTransformType Type;
    public CustomTransform Transform;
}

public class CustomTransform
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public void Set(Transform transform)
    {
        Position = transform.position;
        Rotation = transform.rotation;
        Scale = transform.localScale;
    }
}

public enum PlayerTransformType
{
    None = 0,
    Head = 1,
    LHand = 2,
    RHand = 3,
    Canvas = 4
}

public class IKTransforms
{
    public Vector3 CanvasPosition;
    
    public Vector3 HeadPosition;
    public Quaternion HeadRotation;
    
    public Vector3 HandLPosition;
    public Quaternion HandLRotation;
    
    public Vector3 HandRPosition;
    public Quaternion HandRRotation;
}