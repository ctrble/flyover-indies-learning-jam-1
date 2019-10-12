using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle_Controller : MonoBehaviour {

  public Collider2D paddleCollider;
  public float speed = 5;
  public Vector2 currentDirection = Vector2.zero;
  public LayerMask staticLayer;

  void OnEnable() {
    if (paddleCollider == null) {
      paddleCollider = gameObject.GetComponent<Collider2D>();
    }
  }

  void Update() {
    transform.position = NewPosition();
  }

  Vector3 NewPosition() {
    Vector3 newPosition = transform.position + InputDirection() * speed * Time.deltaTime;

    if (IsBlocked(newPosition)) {
      newPosition = transform.position;
    }

    return newPosition;
  }

  Vector3 InputDirection() {
    float xAxis = Input.GetAxisRaw("Horizontal");
    float yAxis = 0;
    float zAxis = 0;
    return new Vector3(xAxis, yAxis, zAxis);
  }

  bool IsBlocked(Vector3 position) {
    return Physics2D.OverlapBox(position, paddleCollider.bounds.size, 0, staticLayer);
  }
}
