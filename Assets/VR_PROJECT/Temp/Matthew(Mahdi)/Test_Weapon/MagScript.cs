using UnityEngine;

public class MagScript : MonoBehaviour{

    public int capacity = 15;
    public int current = 0;

    public GameObject lastBullet;
    public GameObject have5bullets;
    public GameObject have10bullets;
    public GameObject have15bullets;

    public void init(int bullets){
        current = bullets;
        lastBullet?.SetActive(current>0);
        have5bullets?.SetActive(current>=5);
        have10bullets?.SetActive(current>=10);
        have15bullets?.SetActive(current>=15);
    }

    public void init(){
        init(capacity);
    }
}
