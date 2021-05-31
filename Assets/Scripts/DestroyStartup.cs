using UnityEngine;

public class DestroyStartup : MonoBehaviour
{   
    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        if(destroyTime == 0.0f)
        {
            destroyTime = 1.0f;
        }
        
        Destroy(this.gameObject,destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
