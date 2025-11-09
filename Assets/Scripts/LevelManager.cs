using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour
    //  is created
    [SerializeField] private float enemyCounter;
    [SerializeField] private string leveltoChangeTo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCounter == 0)
        {
            SceneManager.LoadScene(leveltoChangeTo);
        }
    }
    
    public void EnemyChecker()
    {
        enemyCounter--;
    }
}
