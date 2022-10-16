using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlace : MonoBehaviour
{
    [FormerlySerializedAs("_objectToPlace")] [SerializeField] private GameObject objectToPlace;
    private GameObject _spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 _touchPosition;
    private static List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryToGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryToGetTouchPosition(out Vector2 touchPosition)) return;

        if (_arRaycastManager.Raycast(touchPosition, _hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = _hits[0].pose;

            //check if there is an object aready, if so you can move around.
            //you coule also just put another one.. depends on your app

            if (_spawnedObject == null)
            {
                _spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
            }
            // else
            // {
            //     _spawnedObject.transform.position = hitPose.position;
            // }
        }
    }
}
