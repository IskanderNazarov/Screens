using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardIcon : MonoBehaviour {
    [SerializeField] private Image ticket;
    [SerializeField] private TextMeshProUGUI ticketsCount;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private Button button;
    [SerializeField] private Sprite active;
    [SerializeField] private Sprite collected;
    [SerializeField] private Sprite disabled;

    public Action<int> OnClicked;
    public int Index { get; private set; }
    public int Reward { get; private set; }

    public void Init(int reward, int index) {
        Reward = reward;
        Index = index;
        ticketsCount.text = $"x{Reward}";
        dayText.text = $"x{index + 1}";
    }

    public void UpdateItem(DailyItemState state) {
        switch (state) {
            case DailyItemState.collected:
                ticket.sprite = collected;
                break;
            case DailyItemState.readyToCollect:
                ticket.sprite = active;
                break;
            case DailyItemState.notReady:
                ticket.sprite = disabled;
                break;
        }

        button.interactable = state == DailyItemState.readyToCollect;
    }

    public void OnItemClicked() {
        OnClicked?.Invoke(Index);
    }

    public enum DailyItemState {
        collected, 
        readyToCollect,
        notReady
    }
}