using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Arhive {
public class ScopeManager : MonoBehaviour
{
    public int score;
    public Text scoreDisplay;
    
    private void Update() {
        scoreDisplay.text = score.ToString();
    }
}
}