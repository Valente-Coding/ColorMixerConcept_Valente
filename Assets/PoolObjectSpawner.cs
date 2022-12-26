using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PoolObjectSpawner : MonoBehaviour
{
    // Gets The Prefab And Spawn
    [SerializeField] private GameObject prefab; // Prefab to instantiate
    [SerializeField] private Vector3 AnimationOffset; // Offsets the object relative to the SendObjectToBlender() animation
    [SerializeField] private Vector3 AnimationRotation; // Rotates the object before it enters the blender

    private GameObject objSpawned;

    void Start()
    {
        objSpawned = Instantiate(prefab, transform);
        objSpawned.SetActive(false);
    }

    public void SetActiveStatePrefab(bool state)
    {
        objSpawned.SetActive(state);
    }

    public void SendObjectToBlender()
    {
        Sequence objSequence = DOTween.Sequence();
        objSequence.Append(objSpawned.transform.DOMove(new Vector3(-1.174f + AnimationOffset.x, 1.665f + AnimationOffset.y, 5.701f + AnimationOffset.z), 0.5f));
        objSequence.Append(objSpawned.transform.DORotate(AnimationRotation, 0.5f));
        objSequence.Append(objSpawned.transform.DOMove(new Vector3(-1.174f + AnimationOffset.x, 1.182f + AnimationOffset.y, 5.701f + AnimationOffset.z), 0.5f));
        objSequence.Append(objSpawned.transform.DOScale(new Vector3(0, 0, 0), 0.5f));
        objSequence.Append(objSpawned.transform.DOMove(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.5f));
        objSequence.Append(objSpawned.transform.DORotate(new Vector3(prefab.transform.localEulerAngles.x, prefab.transform.localEulerAngles.y, prefab.transform.localEulerAngles.z), 0.5f));
        objSequence.Append(objSpawned.transform.DOScale(new Vector3(prefab.transform.localScale.x, prefab.transform.localScale.y, prefab.transform.localScale.z), 0.5f));

    }

}
