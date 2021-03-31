using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoController : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI enemiesLeftText;
    [SerializeField] private GameController enemyController;

    private void Start()
    {
        enemyController.OnEnemySpawnedEvent += SetEnemyUI;
        SetEnemyUI();
    }

    private void SetEnemyUI()
    {
        enemyController.CurrentEnemy.OnEnemyDamaged += UpdateEnemyHP;
        levelText.text = "Enemy Level: " + enemyController.CurrentEnemy.GetLevel();
        hpSlider.maxValue = enemyController.CurrentEnemy.GetMaxHealth();
        hpSlider.value = hpSlider.maxValue;
        enemiesLeftText.text = "Left: " + (enemyController.KillsToWin - enemyController.EnemyCount);
    }

    private void UpdateEnemyHP()
    {
        hpSlider.value -= enemyController.DamagePerClick;
    }
}
