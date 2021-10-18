using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public bool paused;
    public Slider fovSlider;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        fovSlider.gameObject.SetActive(false);
        paused = false;
        cam.fieldOfView = fovSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !paused)
        {
            Time.timeScale = 0;
            paused = !paused;

            Cursor.lockState = CursorLockMode.None;
            fovSlider.gameObject.SetActive(true);
            return;
        }

        if (Input.GetKeyDown(KeyCode.P) && paused)
        {
            Time.timeScale = 1;
            paused = !paused;

            Cursor.lockState = CursorLockMode.Locked;
            fovSlider.gameObject.SetActive(false);
        }

        cam.fieldOfView = fovSlider.value;

    }
}
