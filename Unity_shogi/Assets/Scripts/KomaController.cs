using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class KomaController : MonoBehaviour
{
    private bool swiped = false;
    private InputManager inputManager;
    private GameSceneManager gameSceneManager;
    private float SwipeForceMultiplier = 10f;
    void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        gameSceneManager = GameObject.Find("GameSceneManager").GetComponent<GameSceneManager>();
    }
    void Update()
    {
        if (!swiped && inputManager.MouseUp)
        {
            SwipeKoma(gameSceneManager.rb);
        }
    }
    public void SwipeKoma(Rigidbody rb)
    {
        //InputManagerのスワイプの距離を参照
        Vector3 SwipeDistance = inputManager.SwipeMouse();
        //スワイプの方向の算出
        Vector3 SwipeDirection = -SwipeDistance.normalized;
        //スワイプの力の算出
        float SwipeForce = SwipeDistance.magnitude * SwipeForceMultiplier;
        //Rigidbodyに力を加える
        rb.AddForce(SwipeDirection * SwipeForce, ForceMode.Impulse);
        swiped = true;
        inputManager.MouseUp = false;
    }
}
