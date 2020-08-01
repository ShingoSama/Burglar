using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;
    private CharacterStats[] stats;
    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
    }
    public void SetStats(params CharacterStats[] charStats)
    {
        stats = charStats;
    }
    public void UpdateStatsValues(PlayerStats playerStats)
    {
        statDisplays[0].ValueText.text = playerStats.Name;
        statDisplays[1].ValueText.text = playerStats.Level.ToString();
        statDisplays[2].ValueText.text = playerStats.Experience.ToString();
        statDisplays[3].ValueText.text = playerStats.GetCurrentHealth().ToString() + " / " + stats[0].Value.ToString();
        for (int i = 4; i < statDisplays.Length; i++)
        {
            statDisplays[i].ValueText.text = stats[i-3].Value.ToString();
        }
    }
}
