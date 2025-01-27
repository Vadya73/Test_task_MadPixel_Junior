using UnityEngine;
using YG;

namespace Localization
{
    public class Localization
    {
        private Language currentLanguage = Language.RU;

        public Language CurrentLanguage => currentLanguage;

        public void SetNextLanguage()
        {
            currentLanguage = GetNextLanguage(currentLanguage);
            ChangeLanguage(currentLanguage);
        }

        public void ChangeLanguage(Language language)
        {
            YG2.SwitchLanguage(language.ToString());
            currentLanguage = language;
            Debug.Log(YG2.lang);
        }

        private Language GetNextLanguage(Language current)
        {
            var values = (Language[])System.Enum.GetValues(typeof(Language));
            int nextIndex = (System.Array.IndexOf(values, current) + 1) % values.Length;
            return values[nextIndex];
        }
    }

    public enum Language
    {
        RU = 0,
        EN = 1,
        CHI = 2,
        JA = 3,
        
    }
}