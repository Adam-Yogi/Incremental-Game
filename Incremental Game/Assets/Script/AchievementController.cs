﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    //instance seperti pada gamemanager, fungsinya adalah membuat sistem singleton
    //dibuat untuk memudahkan pemanggilan script bersifat manager dari script lain

    private static AchievementController _instance = null;
    public static AchievementController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AchievementController>();
            }
            return _instance;
        }
    }

    [SerializeField] private Transform _popUpTransform;
    [SerializeField] private Text _popUpText;
    [SerializeField] private float _popUpShowDuration = 3f;
    [SerializeField] private List<AchievementData> _achivementList;

    private float _popUpShowDurationCounter;

    private void Update()
    {
        if (_popUpShowDurationCounter > 0)
        {
            //kurangi durasi ketika pop up durasi lebih dari 0
            _popUpShowDurationCounter -= Time.unscaledDeltaTime;
            //lerp adalah fungsi linear interpolation, untuk mengubah value secara perlahan
            _popUpTransform.localScale = Vector3.LerpUnclamped(_popUpTransform.localScale, Vector3.one, 0.5f);
        }
        else
        {
            _popUpTransform.localScale = Vector2.LerpUnclamped(_popUpTransform.localScale, Vector3.right, 0.5f);
        }
    }


    public void UnlockAchievement(AchievementType type,string value)
    {
        //mencari data achievement
        AchievementData achievement = _achivementList.Find(a => a.Type == type && a.Value == value);
        if(achievement != null && !achievement.IsUnlocked)
        {
            achievement.IsUnlocked = true;
            ShowAchievementPopUp(achievement);
        }
    }
    private void ShowAchievementPopUp(AchievementData achievement)
    {
        _popUpText.text = achievement.title;
        _popUpShowDurationCounter = _popUpShowDuration;
        _popUpTransform.localScale = Vector2.right;
    }
}

// System.Serializable digunakan agar object dari script bisa di-serialize
// dan bisa di-inputkan dari Inspector, jika tidak terdapat ini, maka variable tidak akan muncul di inspector

[System.Serializable]
public class AchievementData
{
    public string title;
    public AchievementType Type;
    public string Value;
    public bool IsUnlocked;
}

public enum AchievementType
{
    UnlockResource
}
