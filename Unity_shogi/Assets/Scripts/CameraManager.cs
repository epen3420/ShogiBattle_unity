using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  private Vector3 initialPos = new(0, 8, -3.5f);
  private Quaternion initialrotate = Quaternion.Euler(70, 0, 0);

  private Vector3 AllyCameraPos = new Vector3(0.0f, 8.0f, -3.5f);
  private Quaternion AllyCameraRotate = Quaternion.Euler(70f, 0f, 0f);
  private Vector3 EnemyCameraPos = new Vector3(0.0f, 8.0f, 3.5f);
  private Quaternion EnemyCameraRotate = Quaternion.Euler(70f, 180f, 0f);

  void Start()
  {
    transform.position = initialPos;
  }

  public void SetTurnSwitchCamera()
  {
    float camPos = transform.position.z;
    transform.position = new(0, 8, -camPos);
  }
}
