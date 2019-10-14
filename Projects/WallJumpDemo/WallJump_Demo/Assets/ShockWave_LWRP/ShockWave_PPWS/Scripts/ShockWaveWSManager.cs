/*
BlurRenderer_Mobile.cs
Creates global textures that have been passed through the GaussianBlur_Mobile(Hidden).shader
*/


using System.Collections.Generic;
using UnityEngine.Rendering;

/// <summary>
/// A manager that keeps tracks of objects that need to be rendered to the distortion buffer just
/// before rendering our custom after-stack Post Process effect.
/// </summary>
public class ShockWaveWSManager
{
    #region Singleton

    /// <summary>
    /// Singleton backing field.
    /// </summary>
    private static ShockWaveWSManager _instance;

    /// <summary>
    /// Singleton accessor. Replacing this for whatever ServiceLocator/Injection pattern your game
    /// uses would be a good idea when implementing a system like this!
    /// </summary>
    public static ShockWaveWSManager Instance
    {
        get
        {
            return _instance = _instance ?? new ShockWaveWSManager();
        }
    }

    #endregion

    /// <summary>
    /// The collection of distortion effects 
    /// </summary>
    private readonly List<ShockWaveWSEffect> _SWWSEffect = new List<ShockWaveWSEffect>();

    /// <summary>
    /// Registers an effect with the manager.
    /// </summary>
    public void Register(ShockWaveWSEffect SWWSEffect)
    {
        _SWWSEffect.Add(SWWSEffect);
    }

    /// <summary>
    /// Deregisters an effect from the manager.
    /// </summary>
    public void Deregister(ShockWaveWSEffect SWWSEffect)
    {
        _SWWSEffect.Remove(SWWSEffect);
    }

    /// <summary>
    /// Adds the commands which draw the registered renderers to the target CommandBuffer.
    /// </summary>
    public void PopulateCommandBuffer(CommandBuffer commandBuffer)
    {
        for (int i = 0, len = _SWWSEffect.Count; i < len; i++)
        {
            var effect = _SWWSEffect[i];
            commandBuffer.DrawRenderer(effect.Renderer, effect.Material);
        }
    }
}
