using UnityEngine;
 
[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] woodClips;
    [SerializeField] private AudioClip[] grassClips;
    [SerializeField] private AudioClip[] rockClips;
    public static FootSteps instance;
    public Animator animator;
    public Fps_Script controller;
    public CharacterController characterController;
    //public new Rigidbody rigidbody;
 
    private AudioSource audioSource;
 
    private TerrainDetector terrainDetector;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        terrainDetector = new TerrainDetector();
    }
 
    //Step is an event from the Animation itself, everytime the animation fires "Step" , a Clip gets played
    private void Step()
    {
        if(Mathf.Abs(controller.moveDirection.x) > 0.1f || Mathf.Abs(controller.moveDirection.z) > 0.1f  && !animator.GetCurrentAnimatorStateInfo(0).IsName("Running") && characterController.isGrounded == true)
        {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
        }
    }
    private void Run()
    {
        if(Mathf.Abs(controller.moveDirection.x) > 0.1f || Mathf.Abs(controller.moveDirection.z) > 0.1f && !animator.GetCurrentAnimatorStateInfo(0).IsName("Steps") && characterController.isGrounded == true)
        {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
        }
    }
 
    private AudioClip GetRandomClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);
        switch (terrainTextureIndex)
        {
            case 0:
                return woodClips.Length > 0 ? woodClips[Random.Range(0, woodClips.Length)] : null;
            case 1:
                return grassClips.Length > 0 ? grassClips[Random.Range(0, grassClips.Length)] : null;
            case 2:
            default:
                return rockClips.Length > 0 ? rockClips[Random.Range(0, rockClips.Length)] : null;
        }
    }
}