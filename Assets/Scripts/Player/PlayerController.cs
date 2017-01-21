using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSpeed = 1.5f;
    public float turningSpeed = 60;

    [SerializeField]
    private float durationToGainScore = 1f;
    [SerializeField]
    private float minAngleToGainScore = 20f;
    [SerializeField]
    private float maxAngleToGainScore = 35f;

    [SerializeField]
    private string jumpKeyBind = "Jump";

    [SerializeField]
    private ScoreController theScoreController;

    public Vector3 DirToCenter;
    private float angle;
    

    void Start() {
        theScoreController = FindObjectOfType<ScoreController>();

        var center = Stadium.Instance.Center.transform.position;
        DirToCenter = (center - transform.position).normalized;

        DirToCenter.y = 0;
        DirToCenter.Normalize();

    }

    void Update()
    {
        foreach (var wave in Stadium.Instance.Waves)
        {
            angle = Vector3.Angle(-DirToCenter, wave.Direction);
            print("Player  " + angle);
            if (angle < maxAngleToGainScore && angle > minAngleToGainScore)
            {  
                theScoreController.CanGainScore(durationToGainScore);
            }
        }

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
