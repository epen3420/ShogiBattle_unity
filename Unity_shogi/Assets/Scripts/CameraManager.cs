using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  private Vector3 initialPos = new(0, 8, -3.5f);
  private Quaternion initialrotate = Quaternion.Euler(70, 0, 0);

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
