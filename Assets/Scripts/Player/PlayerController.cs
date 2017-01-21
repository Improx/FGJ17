using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {

	private GameObject head;
	public GameObject Head{
		get{
			if (head == null) {
				head = transform.FindChild ("Torso/Body").gameObject;
			}
			return head;
		}
	}

    public float movementSpeed = 1.5f;
    public float turningSpeed = 60;


    [SerializeField]
    private AudioClip cheerSound;
    private AudioSource myAudioSource;
    private bool canPlayAudio = true;

    [SerializeField]
    private float durationToGainScore = 1f;
    [SerializeField]
    private float minAngleToGainScore = 0f;
    [SerializeField]
    private float maxAngleToGainScore = 20f;

    [SerializeField]
    private float scoreGainPerFrame = 20;
    [SerializeField]
    private float scoreReducedPerFrame = 10;


    [SerializeField]
    private string jumpKeyBind = "Jump";

    [SerializeField]
    private ScoreController theScoreController;

    public Vector3 DirToCenter;
    private float angle;

    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<PlayerController>();
            }

            return instance;
        }
    }

    void Start() {
        myAudioSource = GetComponent<AudioSource>();
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
            if (angle < maxAngleToGainScore && angle > minAngleToGainScore)
            {  
                theScoreController.CanGainScore(durationToGainScore);
            }
        }



        if (Input.GetButton(jumpKeyBind))
        {
            if (myAudioSource != null && cheerSound != null && canPlayAudio) {
                myAudioSource.PlayOneShot(cheerSound);
                StartCoroutine(AudioCooldown(1f));
            }
            PlayerDude.Instance.cheerer.Frame = Mathf.Lerp(PlayerDude.Instance.cheerer.Frame, 1.0f, movementSpeed * Time.deltaTime);
            if (theScoreController.CanGainScoreBool)
            {
                theScoreController.addScore(scoreGainPerFrame * Time.deltaTime);
                theScoreController.isShamed = false;
            }
            else {
                theScoreController.reduceScore(scoreReducedPerFrame * Time.deltaTime);
                theScoreController.isShamed = true;
            }
        } else {
            PlayerDude.Instance.cheerer.Frame = Mathf.Lerp(PlayerDude.Instance.cheerer.Frame, 0.0f, movementSpeed * Time.deltaTime);
            if (theScoreController.CanGainScoreBool)
            {
                theScoreController.reduceScore(scoreReducedPerFrame * Time.deltaTime);
                theScoreController.isShamed = true;
            }
            else {
                theScoreController.isShamed = false;
            }
        }
    }


    private IEnumerator AudioCooldown(float duration)
    {
        canPlayAudio = false;
        yield return new WaitForSeconds(duration);
        canPlayAudio = true;


    }
}
