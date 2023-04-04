using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControlScript : MonoBehaviour
{
    public GameObject playerCamObj, finishCamObj;
    public GameObject mainCamera, character;
    
    private CinemachineBrain cinemachineBrain;
    bool isGameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineBrain = mainCamera.GetComponent<CinemachineBrain>();
        character.GetComponent<PlayerController>().enabled = false;
        finishCamObj.SetActive(true);
        playerCamObj.SetActive(false);
        StartCoroutine(FinishToPlayerCam());
    }

    IEnumerator FinishToPlayerCam()
    {
        yield return new WaitForSeconds(2);
        finishCamObj.SetActive(false);
        playerCamObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!cinemachineBrain.IsBlending)
        {
            ICinemachineCamera finishCam = finishCamObj.GetComponent<ICinemachineCamera>();
            bool finishCamLive = CinemachineCore.Instance.IsLive(finishCam);
            if (!finishCamLive && !isGameStarted)
            {
                isGameStarted = true;
                character.GetComponent<PlayerController>().enabled = true;
            }
        }
    }
}
