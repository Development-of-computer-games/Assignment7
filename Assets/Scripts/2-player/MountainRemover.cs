using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MountainRemover : MonoBehaviour
{

    [SerializeField] KeyCode key;
    [SerializeField]  Excavation excavationTiles = null;
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] TileBase tileToChange = null;



    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    // Update is called once per frame
    void Update()
    {




        if (Input.GetKey(KeyCode.DownArrow)){
            this.StartCoroutine(removeTile(transform.position + Vector3.down));
        }

        else if (Input.GetKey(KeyCode.UpArrow)){
            this.StartCoroutine(removeTile(transform.position + Vector3.up));
        }

        else if (Input.GetKey(KeyCode.RightArrow)){
            this.StartCoroutine(removeTile(transform.position + Vector3.right));
        }

        else if(Input.GetKey(KeyCode.LeftArrow)){
            this.StartCoroutine(removeTile(transform.position + Vector3.left));
        }



    }


    private IEnumerator removeTile(Vector3 position)
    {

        if (Input.GetKeyUp(key))
        {

            // converting the vecotr position into the tile position
            TileBase tileOnNewPosition = TileOnPosition(position);

            // if its the tile we want to remove
            if (excavationTiles.Contain(tileOnNewPosition))
            {

                Vector3Int pos = tilemap.WorldToCell(position);


               
                yield return new WaitForSeconds(3);
                tilemap.SetTile(pos, tileToChange);

            }
        }
  

    }
}

