using TMPro;
using UnityEngine;

public class Game : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI finishText;
    [SerializeField] private Map map;
    private int levelIndex;

    public void StartGame(int index) {
        gameObject.SetActive(true);
        levelIndex = index;

        finishText.text = $"Finish level {index + 1}";
    }

    public void FinishGame() {
        PlayerPrefs.SetInt(Map.CUR_LEVEL_KEY, levelIndex + 1);
        gameObject.SetActive(false);
        map.Show();
    }
}