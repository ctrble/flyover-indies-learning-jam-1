using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Brick_Controller : MonoBehaviour {

  public Tilemap tilemap;
  public BoundsInt bounds;
  public TileBase[] allTiles;

  public List<GameObject> bricks;
  public List<GameObject> remainingBricks;
  Dictionary<GameObject, bool> hasSpawned = new Dictionary<GameObject, bool>();


  // public List<GameObject> bricks;
  // public List<GameObject> remainingBricks;
  // Dictionary<GameObject, bool> hasSpawned = new Dictionary<GameObject, bool>();

  public float targetTime = 1f;
  private float currentTime;

  void OnEnable() {

    SetUpBricks();
    currentTime = targetTime;

  }

  void Update() {
    BricksRemaining();
    CountdownToNewBrick();
  }

  void SetUpBricks() {

    // bounds = tilemap.cellBounds;
    // allTiles = tilemap.GetTilesBlock(bounds);

    // for (int x = 0; x < bounds.size.x; x++) {
    //   for (int y = 0; y < bounds.size.y; y++) {
    //     TileBase tile = allTiles[x + y * bounds.size.x];

    //     // GetTileData(allTiles[x + y * bounds.size.x], tilemap, ref TileData tileData) {

    //     // }

    //     if (tile != null) {
    //       Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);

    //       // tile.SetActive(false);
    //       bricks.Add(tile);
    //       hasSpawned.Add(tile, false);
    //     }
    //     else {
    //       Debug.Log("x:" + x + " y:" + y + " tile: (null)");
    //     }
    //   }
    // }

    // for (int i = 0; i < allTiles.Length; i++) {
    //   GameObject brick = gameObject.transform.GetChild(i).gameObject;
    //   brick.transform.SetSiblingIndex(Random.Range(0, gameObject.transform.childCount));
    // }
    // for (int i = 0; i < gameObject.transform.childCount; i++) {
    //   GameObject brick = gameObject.transform.GetChild(i).gameObject;
    //   // brick.transform.SetSiblingIndex(Random.Range(0, gameObject.transform.childCount));
    //   brick.SetActive(false);
    //   bricks.Add(brick);
    //   hasSpawned.Add(brick, false);
    // }

    // original
    for (int i = 0; i < gameObject.transform.childCount; i++) {

      GameObject brick = gameObject.transform.GetChild(i).gameObject;
      brick.transform.SetSiblingIndex(Random.Range(0, gameObject.transform.childCount));
    }

    for (int i = 0; i < gameObject.transform.childCount; i++) {
      GameObject brick = gameObject.transform.GetChild(i).gameObject;
      // brick.transform.SetSiblingIndex(Random.Range(0, gameObject.transform.childCount));

      brick.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

      brick.SetActive(false);
      bricks.Add(brick);
      hasSpawned.Add(brick, false);
    }
  }

  void CountdownToNewBrick() {
    currentTime -= Time.deltaTime;
    if (currentTime <= 0.0f) {
      currentTime = targetTime;

      ActivateNextBrick();
    }
  }

  void ActivateNextBrick() {
    if (bricks.Count != 0) {

      for (int i = 0; i < bricks.Count; i++) {
        GameObject thisBrick = bricks[i];

        if (!hasSpawned[thisBrick]) {
          thisBrick.SetActive(true);
          hasSpawned[thisBrick] = true;
          return;
        }
      }
    }
  }

  void BricksRemaining() {
    if (bricks.Count != 0) {
      for (int i = 0; i < bricks.Count; i++) {

        GameObject thisBrick = bricks[i];
        bool isActive = thisBrick.activeInHierarchy;
        bool inList = remainingBricks.Contains(thisBrick);

        if (isActive && !inList) {
          remainingBricks.Add(thisBrick);
        }
        else if (!isActive && inList) {
          remainingBricks.Remove(thisBrick);
        }

      }
    }
  }
}
