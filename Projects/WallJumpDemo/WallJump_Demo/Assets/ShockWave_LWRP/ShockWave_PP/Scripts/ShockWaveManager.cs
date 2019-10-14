/*
ShockWaveRenderer_Mobile.cs
Creates global textures that have been passed through the GaussianShockWave_Mobile(Hidden).shader
*/


using System.Collections.Generic;
using UnityEngine.Rendering;

/// <summary>
/// A manager that keeps tracks of objects that need to be rendered to the distortion buffer just
/// before rendering our custom after-stack Post Process effect.
/// </summary>
public class ShockWaveManager
{
    #region Singleton

    /// <summary>
    /// Singleton backing field.
    /// </summary>
    private static ShockWaveManager _instance;

    /// <summary>
    /// Singleton accessor. Replacing this for whatever ServiceLocator/Injection pattern your game
    /// uses would be a good idea when implementing a system like this!
    /// </summary>
    public static ShockWaveManager Instance
    {
        get
        {
            return _instance = _instance ?? new ShockWaveManager();
        }
    }

    #endregion

    /// <summary>
    /// The collection of distortion effects 
    /// </summary>
    private readonly List<ShockWaveEffect> _SWEffect = new List<ShockWaveEffect>();

    /// <summary>
    /// Registers an effect with the manager.
    /// </summary>
    public void Register(ShockWaveEffect SWEffect)
    {
        _SWEffect.Add(SWEffect);
    }

    /// <summary>
    /// Deregisters an effect from the manager.
    /// </summary>
    public void Deregister(ShockWaveEffect SWEffect)
    {
        _SWEffect.Remove(SWEffect);
    }

    /// <summary>
    /// Adds the commands which draw the registered renderers to the target CommandBuffer.
    /// </summary>
    public void PopulateCommandBuffer(CommandBuffer commandBuffer)
    {
        for (int i = 0, len = _SWEffect.Count; i < len; i++)
        {
            var effect = _SWEffect[i];
            commandBuffer.DrawRenderer(effect.Renderer, effect.Material);
        }
    }
}
