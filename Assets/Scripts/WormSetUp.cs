using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WormSetUp : MonoBehaviour
{
    public GameObject SetUp;

    public Tile gate1;
    public Tile gate2;
    public Tile gate3;
    public Tile gate4;
    public Tile sand;

    public Tilemap gateTop;
    public Tilemap gateBottom;

    // Start is called before the first frame update
    void Start()
    {
        SetUp.SetActive(true);
        gateTop.SetTile(new Vector3Int(-54, 12, 0), gate1);
        gateTop.SetTile(new Vector3Int(-53, 12, 0), gate2);
        gateTop.SetTile(new Vector3Int(-54, 11, 0), gate3);
        gateTop.SetTile(new Vector3Int(-53, 11, 0), gate4);
        gateBottom.SetTile(new Vector3Int(-54, 10, 0), sand);
        gateBottom.SetTile(new Vector3Int(-53, 10, 0), sand);
    }
}
