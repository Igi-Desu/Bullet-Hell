using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField]public Image icon;
    [SerializeField]public TMP_Text toolTip;
    [SerializeField]public TMP_Text level;
    [SerializeField]public TMP_Text isNew;
    [SerializeField]public TMP_Text itemType;
}
