using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public Button ResourceButton;
    public Image ResourceImage;
    public Text ResourceDescription;
    public Text ResourceUpgradeCost;
    public Text ResourceUnlockCost;

    private ResourceConfig _config;
    private int _level = 1;

    public bool IsUnlocked { get; private set; }

    private void Start()
    {
        ResourceButton.onClick.AddListener(() =>
        {
            if (IsUnlocked)
            {
                UpgradeLevel();
            }
            else
            {
                UnlockResource();
            }
        });
    }

    public void UpgradeLevel()
    {
        double upgradeCost = GetUpgradeCost();
        if(GameManager.instance._totalGold < upgradeCost)
        {
            return;
        }
        GameManager.instance.AddGold(-upgradeCost);
        _level++;

        ResourceUpgradeCost.text = $"Upgrade Cost\n{GetUpgradeCost()}";
        ResourceDescription.text = $"{ _config.name} Lv.{_level}\n+{GetOutput().ToString("0")}";


    }

    public double GetOutput()
    {
        return _config.Output *_level;
    }

    public double GetUpgradeCost()
    {
        return _config.UpgradeCost * _level;
    }

    public double GetUnlockCost()
    {
        return _config.UnlockCost;
    }
    
    public void SetConfig(ResourceConfig config)
    {
        _config = config;
        //ToString("0") agar angka dibelakang koma hilang
        //ex: "0"
        ResourceDescription.text = $"{_config.name} Lv.{_level}\n+{GetOutput().ToString("0")}";
        ResourceUnlockCost.text = $"Unlock Cost\n{_config.UnlockCost}";
        ResourceUpgradeCost.text = $"Upgrade Cost\n{GetUpgradeCost()}";

        SetUnlocked(_config.UnlockCost == 0);
    }

    public void UnlockResource()
    {
        double unlockCost = GetUnlockCost();
        if (GameManager.instance._totalGold < unlockCost)
        {
            return;
        }

        SetUnlocked(true);
        GameManager.instance.ShowNextResource();
        AchievementController.Instance.UnlockAchievement(AchievementType.UnlockResource, _config.name);

    }

    public void SetUnlocked(bool unlocked)
    {
        IsUnlocked = unlocked;
        ResourceImage.color = IsUnlocked ? Color.white : Color.grey;
        ResourceUnlockCost.gameObject.SetActive(!unlocked);
        ResourceUpgradeCost.gameObject.SetActive(unlocked);
    }

}
