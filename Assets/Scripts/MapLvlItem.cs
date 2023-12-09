using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapLvlItem : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI lvlNumberText;
    [SerializeField] private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private Sprite active;
    [SerializeField] private Sprite passed;
    [SerializeField] private Sprite locked;

    public Action<int> OnItemClicked;
    private int itemIndex;

    public void Init(int index) {
        itemIndex = index;
        lvlNumberText.text = $"Lvl.{index + 1}";
    }

    public void UpdateItem(int activeLvlIndex) {
        if (itemIndex < activeLvlIndex) icon.sprite = passed;
        else if (itemIndex > activeLvlIndex) icon.sprite = locked;
        else icon.sprite = active;

        lvlNumberText.gameObject.SetActive(itemIndex <= activeLvlIndex);
        button.interactable = itemIndex <= activeLvlIndex;
    }
    
    public void Clicked() {
        SoundManager.shared.PlayButtonSound();
        OnItemClicked?.Invoke(itemIndex);
    }
}