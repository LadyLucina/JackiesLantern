using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LurkerPortal : MonoBehaviour
{
    
    public Renderer Object;

    public ParticleSystem Portal;

    private void OnTriggerEnter(Collider oher)
    {
        Portal.Play();

    }




        // Start is called before the first frame update
        void Start()
    {
        Portal.Pause();
    }


    public void Dissapear()
    {
        Destroy(Portal);
        Portal.Pause();
    }

    
}
