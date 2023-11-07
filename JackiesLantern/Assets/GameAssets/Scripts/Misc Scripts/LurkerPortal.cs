using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LurkerPortal : MonoBehaviour
{
    
    public Material Material1;
    public Material Material2;
    public Renderer Object;

    public ParticleSystem Portal;

    private void OnTriggerEnter(Collider oher)
    {
        //Object.material = Material1;  //Makes VFX visible
        Portal.Play();

    }




        // Start is called before the first frame update
        void Start()
    {
        Portal.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        


    }


    public void Dissapear()
    {
        //Destroy(LurkerPortal);
        Portal.Pause();
    }

    
}
