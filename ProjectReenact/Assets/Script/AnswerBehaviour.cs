using UnityEngine;

public class AnswerBehaviour : MonoBehaviour
{
    [SerializeField] SimulationBehaviour _simulationBehaviour;
    [SerializeField] GameObject _answerText;
    void Start()
    {
        _simulationBehaviour.OnExcuteEvent += AnswerEvent;
    }

    void AnswerEvent(int id)
    {
        if(id == 3)
            _answerText.SetActive(true);
    }
}
