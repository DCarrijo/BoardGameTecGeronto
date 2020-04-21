using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldReaction : MonoBehaviour
{
    public GameObject ripplesVFX;

    private Material mat;

    private void OnCollisionEnter(Collision bateu)
    {
        var ripples = Instantiate(ripplesVFX, transform) as GameObject;
        ripples.transform.localPosition = Vector3.zero;
        var psr = ripples.GetComponent<ParticleSystemRenderer>();
        mat = psr.material;
        mat.SetVector("_SphereCenter", bateu.contacts[0].point);

        //Destroy(ripples, 2);

        /*if(bateu.gameObject.tag == "Enemy")
        {
            var ripples = Instantiate(ripplesVFX, transform) as GameObject;
            var psr = ripples.transform.GetChild(0).GetComponent<ParticleSystemRenderer>();
            mat = psr.material;
            mat.SetVector("_SphereCenter", bateu.contacts[0].point);

            Destroy(ripples, 2);
        }*/
    }
}
