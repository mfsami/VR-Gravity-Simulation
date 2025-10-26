﻿using UnityEngine;

namespace AdamBielecki.SolarSystem
{
  public class CameraManager : MonoBehaviour
  {

    public GameObject MainCamera;
    public GameObject OrbitCamera;
    public static CameraManager instance = null;

    // Hold the information regarding which camera mode is active. It is needed so some methods won't be called more than once.
    public ConstantValues.CameraMode cameraMode;

    void Awake()
    {
      if (instance == null)
      {
        instance = this;
      }
      else if (instance != this)
      {
        Destroy(gameObject);
      }
    }

    void Start()
    {
      MainCamera.GetComponent<Camera>().enabled = false;
      PlanetManager.instance.DisableSunFlare("Sun");
    }

    public void SwitchToMainCamera()
    {
      OrbitCamera.GetComponent<Camera>().enabled = false;
      MainCamera.GetComponent<Camera>().enabled = true;
      ToggleDetailedCameraInputController(true);
      cameraMode = ConstantValues.CameraMode.Detailed;
      LightManager.instance.ToggleOverviewLight(cameraMode);
    }

    public void SwitchToOrbitCamera()
    {
      Debug.Log(OrbitCamera.GetComponent<Camera>());
      OrbitCamera.GetComponent<Camera>().enabled = true;
      MainCamera.GetComponent<Camera>().enabled = false;
      ToggleDetailedCameraInputController(false);
      cameraMode = ConstantValues.CameraMode.Overview;
      LightManager.instance.ToggleOverviewLight(cameraMode);
      Vector3 cameraPosition = DefaultValues.OverviewCameraPosition();
      OrbitCamera.transform.position = DefaultValues.OverviewCameraPosition();
      OrbitCamera.GetComponent<InputController>().transform.position = DefaultValues.OverviewCameraPosition();

      Debug.Log("Finished switching to orbit view");
    }

    private void ToggleDetailedCameraInputController(bool enabled)
    {
      MainCamera.GetComponent<DetailedCameraInputController>().enabled = enabled;
    }
  }
}