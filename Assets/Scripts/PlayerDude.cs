
class PlayerDude : Dude {

    private static PlayerDude instance;
    public static PlayerDude Instance {
        get {
            if (!instance) {
                instance = FindObjectOfType<PlayerDude>();
            }

            return instance;
        }
    }

    void Start() {
        var center = Stadium.Instance.Center.transform.position;
        DirToCenter = (center - transform.position).normalized;

        DirToCenter.y = 0;
        DirToCenter.Normalize();
    }
}
