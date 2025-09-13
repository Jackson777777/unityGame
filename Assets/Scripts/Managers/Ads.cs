//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Advertisements;
//using UnityEngine.UI;

//public class Ads : MonoBehaviour, IUnityAdsListener
//{

//#if UNITY_ANDROID
//    private string gameID = "5356105";

//#elif UNITY_IOS
//    private string gameID = "5356104";
//#endif

//    Button adsButton;
//    public string placementID = "rewardedVideo";
//    // Start is called before the first frame update
//    void Start()
//    {
//        adsButton=GetComponent<Button>();
//        adsButton.interactable = Advertisement.IsRead(placementID);

//        if(adsButton)
//        {
//            adsButton.onClick.AddListener(ShowAds);
//        }

//        Advertisement.AddListener(this);
//        Advertisement.Initialize(gameID, true);
//    }
//    public void ShowAds()
//    {
//        Advertisement.Show(placementID);
//    }

//    public void OnUnityAdsDidError(string message)
//    {

//    }
//    public void OnUnityAdsDidFinish(string placementId,ShowResult showResult)
//    {
//        switch(showResult)
//        {
//            case ShowResult.Failed:
//                break;
//            case ShowResult.Skipped:
//                break;
//            case ShowResult.Finished:
//                Debug.Log("ads over");
//                break;
//        }
//    }
//    public void OnUnityAdsDidStart(string placementId)
//    {

//    }
//    public void OnUnityAdsReady(string placementId)
//    {
//        if(Advertisement.IsReady(placementID))
//        {
//            Debug.Log("Ads ready");
//        }

//    }


//}
