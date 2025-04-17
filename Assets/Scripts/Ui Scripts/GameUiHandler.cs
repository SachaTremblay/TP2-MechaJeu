using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameUiHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerControls;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Destroy(PlayerControls);
    }
}
