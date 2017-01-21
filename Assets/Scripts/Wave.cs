
using UnityEngine;

[System.Serializable]
public class Wave {
    public Vector3 Origin;
    public float DirAngle;
    public float Speed;
    public float ConeAngle;
    public bool Reverse;

    public Wave(Vector3 origin, float speed, float cone, float dirAngle, bool rev) {
        Origin = origin;
        ConeAngle = cone;
        Speed = speed;
        DirAngle = dirAngle;
        Reverse = rev;

        //WaveVisualizer.Instance.wave = this;
    }

    public bool IsDone {
        get {
            return false;
        }
    }

    public void Tick (float delta) {
        if (Reverse) {
            DirAngle = DirAngle - delta * Speed;
        } else {
            DirAngle = DirAngle + delta * Speed;
        }
    }

    public Vector3 Direction {
        get {
            return new Vector3(Mathf.Sin(Mathf.Deg2Rad * DirAngle), 0, Mathf.Cos(Mathf.Deg2Rad * DirAngle)).normalized;
        }
    }
}
