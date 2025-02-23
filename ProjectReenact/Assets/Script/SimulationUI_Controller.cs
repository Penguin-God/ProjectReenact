using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SimulationUI_Controller : MonoBehaviour
{
    TextMeshProUGUI _objtext1;
    TextMeshProUGUI _objtext2;
    Button _linkBtn;
    [SerializeField] SimulationBehaviour simulationBehaviour;
    List<int> selectObjIDs = new List<int>();

    void Start()
    {
        _objtext1 = GetComponentsInChildren<TextMeshProUGUI>()[0];
        _objtext2 = GetComponentsInChildren<TextMeshProUGUI>()[1];
        _linkBtn = GetComponentInChildren<Button>();
        _linkBtn.onClick.AddListener(TrySimulation);
    }

    public void UpdateSelectObjText(int clickId)
    {
        if (selectObjIDs.Count >= 2) return;

        selectObjIDs.Add(clickId);
        if (selectObjIDs.Count == 1) _objtext1.text = selectObjIDs[0].ToString();
        else if(selectObjIDs.Count == 2)
        {
            _objtext1.text = selectObjIDs[0].ToString();
            _objtext2.text = selectObjIDs[1].ToString();
        }
    }

    void TrySimulation()
    {
        if(selectObjIDs.Count < 2) return;

        simulationBehaviour.TryReenact(selectObjIDs);
        selectObjIDs.Clear();
        _objtext1.text = "";
        _objtext2.text = "";
    }
}
