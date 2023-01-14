using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Extensions;
using UnityEngine;

public class FireBaseRemoteConfig : MonoBehaviour
{
	Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;

	private void Start()
	{
		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
			dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available)
			{
				InitializeFirebase();
			}
			else
			{
				Debug.LogError(
				  "Could not resolve all Firebase dependencies: " + dependencyStatus);
			}
		});
	}

	private void InitializeFirebase()
	{
		Debug.Log("Remote config ready!");
	}

	public void FetchFireBase() => FetchDataAsync();

	public string GetUrl()
	{
		return Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("url").StringValue;
	}
	
	public Task FetchDataAsync()
	{
		Debug.Log("Fetching data...");
		System.Threading.Tasks.Task fetchTask =
		Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
			TimeSpan.Zero);
		return fetchTask.ContinueWithOnMainThread(FetchComplete);
	}

	void FetchComplete(Task fetchTask)
	{
		if (fetchTask.IsCanceled)
		{
			Debug.Log("Fetch canceled.");
		}
		else if (fetchTask.IsFaulted)
		{
			Debug.Log("Fetch encountered an error.");
		}
		else if (fetchTask.IsCompleted)
		{
			Debug.Log("Fetch completed successfully!");
		}

		var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
		switch (info.LastFetchStatus)
		{
			case Firebase.RemoteConfig.LastFetchStatus.Success:
				Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
				.ContinueWithOnMainThread(task => {
					Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
								   info.FetchTime));
				});

				break;
			case Firebase.RemoteConfig.LastFetchStatus.Failure:
				switch (info.LastFetchFailureReason)
				{
					case Firebase.RemoteConfig.FetchFailureReason.Error:
						Debug.Log("Fetch failed for unknown reason");
						break;
					case Firebase.RemoteConfig.FetchFailureReason.Throttled:
						Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
						break;
				}
				break;
			case Firebase.RemoteConfig.LastFetchStatus.Pending:
				Debug.Log("Latest Fetch call still pending.");
				break;
		}
	}

}
