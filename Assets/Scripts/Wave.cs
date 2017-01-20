
[System.Serializable]
public class Wave {
    public float wavePos;
    public float waveSpeed;
    public float waveLength;
    public bool reverse;
    
    public Wave(float speed, float length, bool rev) {
        waveLength = length;
        waveSpeed = speed;
        reverse = rev;
        if (reverse) {
            wavePos = Stadium.Instance.rowLength + waveLength;
        } else {
            wavePos = -waveLength;
        }
    }

    public Wave(float pos, float speed, float length) {
        waveLength = length;
        waveSpeed = speed;
        wavePos = pos;
    }

    public bool IsDone {
        get {
            if (reverse) {
                return wavePos <= -waveLength;
            } else {
                return wavePos >= (Stadium.Instance.rowLength + waveLength);
            }
        }
    }

    public void Tick (float delta) {
        wavePos = delta * waveSpeed + wavePos;
    }
}
