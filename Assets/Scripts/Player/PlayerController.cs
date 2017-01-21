using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSpeed = 1.5f;
    public float turningSpeed = 60;
    [SerializeField]
    private string jumpKeyBind = "Jump";

    [SerializeField]
    private ScoreController theScoreController;

    void Start() {
        theScoreController = FindObjectOfType<ScoreController>();
    }

    void Update()
    {

        if (Input.GetButton(jumpKeyBind))
        {
            PlayerDude.Instance.cheerer.Frame = Mathf.Lerp(PlayerDude.Instance.cheerer.Frame, 1.0f, movementSpeed * Time.deltaTime);
            if (theScoreController.CanGainScoreBool)
            {
                theScoreController.addScore(1);
            }
            else {
                theScoreController.reduceScore(1);
            }
        } else {
            PlayerDude.Instance.cheerer.Frame = Mathf.Lerp(PlayerDude.Instance.cheerer.Frame, 0.0f, movementSpeed * Time.deltaTime);
        }
    }

}
