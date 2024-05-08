using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
   public float levelRestartDelay = 2f;

   public void EndGame()
   {
       RestartLevel();
   }

   void RestartLevel()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
