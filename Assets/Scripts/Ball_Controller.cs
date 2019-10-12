using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour {

  public float speed = 5;
  public Vector2 currentDirection = Vector2.down;

  void Update() {
    Move();
  }

  void Move() {
    Vector3 direction = currentDirection;
    transform.position += direction * speed * Time.deltaTime;
  }

  void OnCollisionEnter2D(Collision2D collision) {
    Vector2 newDirection = Vector2.zero;
    if (collision.contactCount > 1) {
      int totalContacts = collision.contactCount;
      ContactPoint2D[] contacts = new ContactPoint2D[totalContacts];
      int contactCount = collision.GetContacts(contacts);
      Vector2 normals = Vector2.zero;
      for (int i = 0; i < totalContacts; i++) {
        normals += contacts[i].normal;
      }
      newDirection = normals / contactCount;
    }
    else {
      newDirection = Vector2.Reflect(currentDirection, collision.GetContact(0).normal);
    }
    currentDirection = newDirection;

    if (collision.gameObject.CompareTag("Brick")) {
      collision.gameObject.SetActive(false);
    }
  }
}
