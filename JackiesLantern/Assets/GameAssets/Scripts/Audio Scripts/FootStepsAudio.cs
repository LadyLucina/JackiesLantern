using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] pavementClips;
    [SerializeField]
    private AudioClip[] grassClips;

    private AudioSource audioSource;
    private TerrainDetector terrainDetector;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        terrainDetector = new TerrainDetector();
    }

    public void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position); //Changes footstep audio depending on terrain
        switch (terrainTextureIndex)
        {
            case 0:
                return grassClips[UnityEngine.Random.Range(0, grassClips.Length)];
            case 1:
                return pavementClips[UnityEngine.Random.Range(0, pavementClips.Length)];
            default:
                return pavementClips[UnityEngine.Random.Range(0, pavementClips.Length)];
        }

    }
}
