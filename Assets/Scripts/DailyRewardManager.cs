using System;
using UnityEngine;

public class DailyRewardManager : MonoBehaviour {
    public static DailyRewardManager shared { private set; get; }
    private readonly int[] REWARDS = {5, 5, 10, 10, 15, 20, 25};
    private const string PREV_DAY_KEY = "prev_day_key";
    private const string AVAILABLE_INDEX_KEY = "available_index_key";
    private const string IS_COLLECTED = "is_collected_key";

    public int AvailableIndex { get; private set; } //indicates that reward available up to this day index
    private int collectedStates; //each bit indicates weather the 'i'-th bit collected

    private void Awake() {
        if (shared == null) {
            shared = this;
        } else if (shared != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        var curDay = (int) DateTime.Now.DayOfWeek;
        //make Monday the first day
        curDay--;
        if (curDay < 0) curDay = 6;
        
        
        var prevDay = PlayerPrefs.GetInt(PREV_DAY_KEY, -1);
        AvailableIndex = PlayerPrefs.GetInt(AVAILABLE_INDEX_KEY, curDay);
        collectedStates = PlayerPrefs.GetInt(IS_COLLECTED, collectedStates);

        if (curDay == 0) {
            AvailableIndex = 0;
            collectedStates = 0;
            //if it's Monday then reset all the states
        }
        else if (curDay != prevDay) {
            //new day has come, new reward available
            AvailableIndex = curDay;
            PlayerPrefs.SetInt(AVAILABLE_INDEX_KEY, curDay);
            PlayerPrefs.SetInt(PREV_DAY_KEY, curDay);
        }
        
        print("curDay: " + curDay);
        print("Available: " + AvailableIndex);
    }

    public void CollectReward(int dayIndex) {
        collectedStates |= 1 << dayIndex;
        PlayerPrefs.SetInt(IS_COLLECTED, collectedStates);
    }

    public DailyRewardIcon.DailyItemState GetDayRewardState(int dayIndex) {
        if(dayIndex > AvailableIndex)return DailyRewardIcon.DailyItemState.notReady;

        var v = (1 << dayIndex) & collectedStates;
        return v == 0 ? DailyRewardIcon.DailyItemState.readyToCollect : DailyRewardIcon.DailyItemState.collected;
    }

    public int GetReward(int dayIndex) {
        return REWARDS[dayIndex];
    }
}