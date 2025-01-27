using System;
using Game.UI;
using Mangers.Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI 
{
    public class SettingMenu : MenuWindow 
    {

        [SerializeField] InGameUIManager inGameUIManager;

      
        [SerializeField] Button restartButton;
        [SerializeField] Button backToMenuButton;


        [SerializeField] Button okButton;
        [SerializeField] Button closeButton;
        [Header("music")]
        [SerializeField] Button musicButton;
        [SerializeField] Image musicOff;
        [SerializeField] Image musicOn;

        private MusicManager musicManager;

        public override void Init(bool isOpen = false) 
        {
            musicManager = ServiceLocator.GetService<MusicManager>();

            base.Init(isOpen);
            okButton.onClick.AddListener(CloseSettingMenu);
            closeButton.onClick.AddListener(CloseSettingMenu);

            restartButton.onClick.AddListener(RestartGame);
            backToMenuButton.onClick.AddListener(BackToMenu);
            musicButton.onClick.AddListener(ChangeMusic);

            musicOff.gameObject.SetActive(true);
            musicOn.gameObject.SetActive(false);
            UpdateMusicIconState();
        }

        private void OnDestroy()
        {
            okButton.onClick.RemoveListener(CloseSettingMenu);
            closeButton.onClick.RemoveListener(CloseSettingMenu);

            restartButton.onClick.RemoveListener(RestartGame);
            backToMenuButton.onClick.RemoveListener(BackToMenu);
            musicButton.onClick.RemoveListener(ChangeMusic);
        }

        private void CloseSettingMenu() => inGameUIManager.CloseSetting();

        private void RestartGame() => inGameUIManager.inGameManager.RestartGame();

        private void BackToMenu() => inGameUIManager.inGameManager.BackToMenu();

        private void ChangeMusic() => MusicSwitcher();
        
        private void MusicSwitcher() 
        {
            musicOff.gameObject.SetActive(!musicManager.IsPlaying);
            musicOn.gameObject.SetActive(musicManager.IsPlaying);

            musicManager.SwitchPlayingMusic();
        }

        private void UpdateMusicIconState()
        {
            if (musicManager == null)
            {
                Debug.LogError("MusicManager не инициализирован.");
                return;
            }

            bool musicPlayingState = musicManager.IsPlaying;

            musicOff.gameObject.SetActive(!musicPlayingState);
            musicOn.gameObject.SetActive(musicPlayingState);
        }

    }
}