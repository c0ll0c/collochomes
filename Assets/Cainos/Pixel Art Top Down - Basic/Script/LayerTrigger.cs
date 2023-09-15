using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //when object exit the trigger, put it to the assigned layer and sorting layers
    //used in the stair objects for player to travel between layers
    public class LayerTrigger : MonoBehaviour
    {
        public string layer;
        public string sortingLayer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            /*
            if (other.gameObject.layer == 3)
            {
                Debug.Log("player & stair" + sortingLayer);
                other.gameObject.transform.Find("attack range").gameObject.layer = LayerMask.NameToLayer(layer);
                other.gameObject.transform.Find("attack range").gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
            }
            */
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer != 3)
            {
                other.gameObject.layer = LayerMask.NameToLayer(layer);

                other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
                SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer sr in srs)
                {
                    sr.sortingLayerName = sortingLayer;
                }
            }
        }

    }
}