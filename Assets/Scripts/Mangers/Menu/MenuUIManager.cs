using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Mangers.Menu 
{
    public class MenuUIManager : MonoBehaviour 
    {
        public MenuManager menuManager;

        [SerializeField] private Button purchaseButton;
        [SerializeField] private Button playGameButton;
        [SerializeField] private Button languageSwitcherButton;
        [SerializeField] private TMP_Text languageTextComponent;

        private Localization.Localization localization;
        public void Init()
        {
            localization = ServiceLocator.GetService<Localization.Localization>();
            
            playGameButton.onClick.AddListener(PlayGame);
            languageSwitcherButton.onClick.AddListener(ChangeLanguage);
            purchaseButton.onClick.AddListener(Purchase);
            
            languageTextComponent.text = localization.CurrentLanguage.ToString();
        }

        private void OnDestroy()
        {
            playGameButton.onClick.RemoveListener(PlayGame);
            languageSwitcherButton.onClick.RemoveListener(ChangeLanguage);
            purchaseButton.onClick.RemoveListener(Purchase);
        }

        private void PlayGame() => menuManager.PlayGame();

        private void ChangeLanguage()
        {
            menuManager.ChangeLanguage();
            languageTextComponent.text = localization.CurrentLanguage.ToString();
        }

        private void Purchase()
        {
            if (YG2.saves.buyAdvDeleter) 
                return;
            
            YG2.BuyPayments("1");
            YG2.saves.buyAdvDeleter = true;
        }
    }
}