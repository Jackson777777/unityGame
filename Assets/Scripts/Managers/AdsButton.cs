//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor.UI;
//using UnityEditor.Advertisements;
//using UnityEngine.Advertisements;
//using UnityEngine.UI;

//public class AdsButton : MonoBehaviour, IUnityAdsInitializationListener
//{
//#if UNITY_ANDROID
//    private string gameID = "5356105";

//#elif UNITY_IOS
//    private string gameID = "5356104";
//#endif

//    Button adsButton;
//    public string placementID = "rewardedVideo";

//    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
//    {
//        //�����Ҫ�Ľ�����������һ���ж���ʲô��棬����ͬ�Ľ���
//        FindObjectOfType<PlayerController>().health = 2;
//            FindObjectOfType<PlayerController>().isDead = false;
//            UIManager.instance.UpdateHealth(FindObjectOfType<PlayerController>().health);

//    }
//    private void Awake()
//    {
//        //�����2������true������Թ�棬false������ʵ���
//        //���һ������this��ʾ��Ӽ���������ɰ汾 Advertisement.AddListener(this);

//        //Advertisement.Initialize(gameID, false, this);
//    }
//    private void Start()
//    {
//        //Reward����޷�����
//        Advertisement.Load(/*rewardPlacement*/"rewardedVideo");
//        //Interstitial��������
//        //Advertisement.Load(interPlacement, this);
//    }
//    public void OnInitializationComplete()
//    {
//        //throw new System.NotImplementedException();
//        Advertisement.Show(placementID);
//        Debug.Log("Unity Ads initialization complete.");
//    }

//    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
//    {
//        //throw new System.NotImplementedException();
//    }

//    //void Start()
//    //{
//    //    adsButton = GetComponent<Button>();
//    //    //adsButton.interactable = Advertisement.IsRead(placementID);

//    //    if (adsButton)
//    //    {
//    //        adsButton.onClick.AddListener(ShowAds);
//    //    }

//    //    //Advertisement.AddListener(this);//����
//    //    Advertisement.Initialize(gameID, true);
//    //}
//    //public void ShowAds()
//    //{
//    //    Advertisement.Show(placementID);
//    //}

//    //public void OnUnityAdsShowClick(string placementId)
//    //{
//    //}

//    //public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
//    //{

//    //    FindObjectOfType<PlayerController>().health = 2;
//    //    FindObjectOfType<PlayerController>().isDead = false;
//    //    UIManager.instance.UpdateHealth(FindObjectOfType<PlayerController>().health);
//    //}

//    //public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
//    //{
//    //}

//    //public void OnUnityAdsShowStart(string placementId)
//    //{
//    //    //Advertisement.Show(placementID);
//    //    //throw new System.NotImplementedException();
//    //}


//}
