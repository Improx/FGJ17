
using UnityEngine;

[System.Serializable]
public class Wave {
    public Vector3 Origin;
    public float DirAngle;
    public float Speed;
    public float ConeAngle;
    public bool Reverse;

    private float startAngle;
    public Wave(Vector3 origin, float speed, float cone, float dirAngle, bool rev) {
        Origin = origin;
        ConeAngle = cone;
        Speed = speed;
        DirAngle = startAngle = dirAngle;
        Reverse = rev;

        
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
            Debug.Log("reverse " + Mathf.Abs(DirAngle - startAngle));
        } else {
            DirAngle = DirAngle + delta * Speed;
            Debug.Log("normal " + (DirAngle - startAngle));
        }
    }

    public Vector3 Direction {
        get {
            return new Vector3(Mathf.Sin(Mathf.Deg2Rad * DirAngle), 0, Mathf.Cos(Mathf.Deg2Rad * DirAngle)).normalized;
        }
    }
}
