/*
BlurEffect.cs
this script controls the enabling and disabling or the blur effect
*/

using UnityEngine;

/// <summary>
/// A component used to register a renderer to be drawn during the Distortion Post Process pass.
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class ShockWaveWSEffect : MonoBehaviour
{
    /// <summary>
    /// The attached Renderer component.
    /// </summary>
    public Renderer Renderer { get; private set; }


    /// <summary>
    /// The primary material on the renderer.
    /// Currently draws all submeshes with the same material and does not support the materials
    /// array for different submesh indices.
    /// </summary>
    public Material Material { get; private set; }


    public Material OriginalMaterial;

    #region AnimationVariables
    /// <summary>
    /// The speed. if speed is zero animation will not run, and can be animated with animator
    /// </summary>
    //[Range(0.01f, 100f)]
    public float speed = 1f;

    /// <summary>
    /// The t, the time for this object
    /// </summary>
    public float t = 0f;


    /// <summary>
    /// The radius over time.
    /// </summary>
    public AnimationCurve radiusAnim;

    /// <summary>
    /// The amplitude over time.
    /// </summary>
    public AnimationCurve amplitudeAnim;

    /// <summary>
    /// The wave size over time.
    /// </summary>
    public AnimationCurve wavesizeAnim;

    ///// <summary>
    ///// color over time
    ///// </summary>
    //public Gradient colorAnim;

    ///// <summary>
    ///// Saturation over time
    ///// </summary>
    //public AnimationCurve SatAnim;

    #endregion


    /// <summary>
    /// Caches values and registers with the manager when enabled. Disables the renderer component
    /// because it should only be visible to the Post Process pass.
    /// </summary>
    private void OnEnable()
    {

        Renderer = GetComponent<Renderer>();
        Renderer.enabled = false;
        //Material = Renderer.sharedMaterial;
        Material = Instantiate<Material>(OriginalMaterial);
        Renderer.material = Material;
        ShockWaveWSManager.Instance.Register(this);
    }

    /// <summary>
    /// Deregisters the effect from the manager.
    /// </summary>
    private void OnDisable()
    {
        ShockWaveWSManager.Instance.Deregister(this);
    }

    
    // Start is called before the first frame update
    void Start()
    {

        //set the radius amplitude and wavesize to zero
        Material.SetFloat("radius", radiusAnim.Evaluate(0.0f));
        Material.SetFloat("amplitude", amplitudeAnim.Evaluate(0.0f));
        Material.SetFloat("wavesize", wavesizeAnim.Evaluate(0.0f));
        //Material.SetColor("tint", colorAnim.Evaluate(0.0f));
        //Material.SetFloat("saturation", SatAnim.Evaluate(0.0f));


        t = 0;
    }

    // Update is called once per frame
    //void Update()
    void FixedUpdate()
    {

        //update the radius amplitude and waveSize
        Material.SetFloat("radius", radiusAnim.Evaluate(t));
        Material.SetFloat("amplitude", amplitudeAnim.Evaluate(t));
        Material.SetFloat("wavesize", wavesizeAnim.Evaluate(t));
        //Material.SetColor("tint", colorAnim.Evaluate(t));
        //Material.SetFloat("saturation", SatAnim.Evaluate(t));

        if (speed == 0f)
        {
            return;
        }

        //increment t
        t += (speed * Time.deltaTime);

        if (t > 1f)
        {
            Destroy(gameObject);
        }

    }


#if UNITY_EDITOR || UNITY_EDITOR_OSX || UNITY_EDITOR_64
    [Range(0.0f, 1.0f)]
    public float timePreview_InEditModeOnly = 0f;
    void Update()
    {
        //not while the editor is in play mode
        if (!Application.isPlaying)
        {
            //GetComponent<MeshRenderer>().material = mat;
            //mat = GetComponent<MeshRenderer>().sharedMaterial;

            Material.SetFloat("radius", radiusAnim.Evaluate(timePreview_InEditModeOnly));
            Material.SetFloat("amplitude", amplitudeAnim.Evaluate(timePreview_InEditModeOnly));
            Material.SetFloat("wavesize", wavesizeAnim.Evaluate(timePreview_InEditModeOnly));
            //Material.SetColor("tint", colorAnim.Evaluate(timePreview_InEditModeOnly));
            //Material.SetFloat("saturation", SatAnim.Evaluate(timePreview_InEditModeOnly));
        }
    }
#endif

}
