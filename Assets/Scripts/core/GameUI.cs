using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{

	public Text m_txtScore;

 
    void Start()
    {
        UpdateUI( Stats.Instance.scores );
    }

	public void UpdateUI( int scores )
    {
		m_txtScore.text= scores.ToString();
    }

}
