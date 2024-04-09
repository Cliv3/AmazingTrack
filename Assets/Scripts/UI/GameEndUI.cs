using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AmazingTrack
{
    public class GameEndUI : MonoBehaviour
    {
        [SerializeField] Text scoreText;

        [Inject] private PlayerStatService playerStatService;
        [Inject] private GameSystem gameSystem;
        public GameObject email;
        public GameObject discountsuccess;
        public GameObject discountfailure;
        public GameObject playagain;



        private void OnEnable()
        {
            ref var playerStatComponent = ref playerStatService.GetPlayerStat();

            if (playerStatComponent.Score >= 300)
            {
                playagain.SetActive(false);
                email.SetActive(true);
            }
                string text = "" + playerStatComponent.Score;
            bool newRecord = playerStatComponent.Score == playerStatComponent.HighScore;
            if (newRecord)
                text += "\nNew record !";

            scoreText.text = text;
        }
        public void OnPlayAgainButtonClicked()
        {
            gameSystem.GameStart(true);
            email.SetActive(false);
            discountsuccess.SetActive(false);
            discountfailure.SetActive(false);
        }
    
        
    }
}