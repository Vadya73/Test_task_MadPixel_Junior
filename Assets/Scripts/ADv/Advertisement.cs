using YG;

namespace ADv
{
    public class Advertisement
    {
        public void ShowInterstitialADv()
        {
            if (YG2.saves.buyAdvDeleter)
                return;
            
            YG2.InterstitialAdvShow();
        }
    }
}
