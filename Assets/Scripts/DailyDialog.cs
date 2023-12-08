using TMPro;
using UnityEngine;

public class DailyDialog : MonoBehaviour {
    [SerializeField] private DailyRewardIcon[] icons;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private RectTransform progressTr;
    [SerializeField] private float progressStartPos;
    [SerializeField] private float progressEndPos;

    private bool isInit;

    private void Init() {
        isInit = true;
        for (var i = 0; i < icons.Length; i++) {
            icons[i].OnClicked += ItemClicked;
            icons[i].Init(DailyRewardManager.shared.GetReward(i), i);
        }
    }

    public void Show() {
        if (!isInit) Init();

        //means that new daily reward available
        gameObject.SetActive(true);

        UpdateItems();
    }

    private void UpdateItems() {
        var dm = DailyRewardManager.shared;
        for (var i = 0; i < icons.Length; i++) {
            var itemState = dm.GetDayRewardState(i);
            icons[i].UpdateItem(itemState);
        }

        progressText.text = $"{dm.AvailableIndex + 1}/7";
        var x = Mathf.Lerp(progressStartPos, progressEndPos, dm.AvailableIndex / 6f);
        var y = progressTr.anchoredPosition.y;
        progressTr.anchoredPosition = new Vector2(x, y);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void ItemClicked(int index) {
        DailyRewardManager.shared.CollectReward(index);
        UpdateItems();
        //todo add tickets
    }
}