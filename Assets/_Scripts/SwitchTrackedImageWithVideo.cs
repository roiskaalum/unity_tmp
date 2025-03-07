using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class SwitchTrackedImageWithVideo : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject videoPrefab; // Prefab with VideoPlayer component

    private Dictionary<ARTrackedImage, GameObject> spawnedVideos = new Dictionary<ARTrackedImage, GameObject>();

    private void Awake()
    {
        if (trackedImageManager == null)
        {
            trackedImageManager = GetComponent<ARTrackedImageManager>();
        }
    }

    private void OnEnable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
    }

    private void OnDisable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            SpawnVideoPlayer(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            HandleTrackingState(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            RemoveVideoPlayer(trackedImage);
        }
    }

    private void SpawnVideoPlayer(ARTrackedImage trackedImage)
    {
        if (!spawnedVideos.ContainsKey(trackedImage))
        {
            GameObject videoInstance = Instantiate(videoPrefab, trackedImage.transform);
            videoInstance.transform.localPosition = Vector3.zero;
            videoInstance.transform.localRotation = Quaternion.identity;

            VideoPlayer videoPlayer = videoInstance.GetComponent<VideoPlayer>();
            videoPlayer.Play();

            spawnedVideos.Add(trackedImage, videoInstance);
        }
    }

    private void HandleTrackingState(ARTrackedImage trackedImage)
    {
        if (spawnedVideos.TryGetValue(trackedImage, out GameObject videoObject))
        {
            videoObject.SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }
    }

    private void RemoveVideoPlayer(ARTrackedImage trackedImage)
    {
        if (spawnedVideos.TryGetValue(trackedImage, out GameObject videoObject))
        {
            Destroy(videoObject);
            spawnedVideos.Remove(trackedImage);
        }
    }
}
