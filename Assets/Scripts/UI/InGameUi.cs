using Game.UI;
using Mangers.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI 
{
    public class InGameUi : MenuWindow 
    {
        [SerializeField] private InGameUIManager inGameUIManager;
        [SerializeField] private Button settingButton;
        [SerializeField] private TextMeshProUGUI currScoreText;
        [SerializeField] private TextMeshProUGUI recordScoreText;

        public override void Init(bool isOpen = false) 
        {
            base.Init(isOpen);
            settingButton.onClick.AddListener(OpenSetting);
        }

        private void OnDestroy()
        {
            settingButton.onClick.RemoveListener(OpenSetting);
        }

        private void OpenSetting() => inGameUIManager.OpenSetting();

        public void SetScore(int score, int hightScore) 
        {
            currScoreText.text = "Current Score:<br>" + score.ToString();
            recordScoreText.text = "Hight score is feoiiofdk: <br>" + hightScore.ToString();
        }
    }
}