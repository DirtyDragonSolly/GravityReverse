using UnityEngine;
using UnityEngine.Networking;

public class AppManager : MonoBehaviour
{
    public GameObject game;
    public GameObject loading;
    public GameObject needNetwork;

    void CheckUrl()
    {
        if (PlayerPrefs.HasKey("Url"))
        {
			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
                loading.SetActive(false);
                needNetwork.SetActive(true);
			}
            else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork 
                || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                loading.SetActive(false);
            }
		}
        else if (!PlayerPrefs.HasKey("Url"))
        {
			
		}
    }
    
    
}
