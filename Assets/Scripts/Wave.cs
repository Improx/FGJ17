
using UnityEngine;

[System.Serializable]
public class Wave {
    public Vector3 Origin;
    public float DirAngle;
    public float Speed;
    public float ConeAngle;
    public bool Reverse;
	public WaveSoundSource SoundSource;

    private float startAngle;



    public Wave(Vector3 origin, float speed, float cone, float dirAngle, bool rev, WaveSoundSource source) {
        Origin = origin;
        ConeAngle = cone;
        Speed = speed;
        DirAngle = startAngle = dirAngle;
        Reverse = rev;
		SoundSource = source;
        
        //WaveVisualizer.Instance.wave = this;
    }

    public bool IsDone {
        get {
            if (Reverse) {
                return Mathf.Abs(DirAngle - startAngle) >= 360;
            } else {
                return DirAngle - startAngle >= 360;
            }
        }
    }

    public void Tick (float delta) {
        if (Reverse) {
            DirAngle = DirAngle - delta * Speed;
        } else {
            DirAngle = DirAngle + delta * Speed;
        }

		float angleInRadians = Mathf.Deg2Rad * DirAngle;
		SoundSource.transform.position = new Vector3((float)Mathf.Sin(angleInRadians) * StadiumSpawner.Instance.midAreaSize.y, 5f, (float)Mathf.Cos(angleInRadians) * StadiumSpawner.Instance.midAreaSize.y);
    }



    public Vector3 Direction {
        get {
            return new Vector3(Mathf.Sin(Mathf.Deg2Rad * DirAngle), 0, Mathf.Cos(Mathf.Deg2Rad * DirAngle)).normalized;
        }
    }
}
