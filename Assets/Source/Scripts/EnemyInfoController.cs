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
        enemyController.currentEnemy.OnEnemyDamaged += UpdateEnemyHP;
        levelText.text = "Enemy Level: " + enemyController.currentEnemy.GetLevel();
        hpSlider.maxValue = enemyController.currentEnemy.GetMaxHealth();
        hpSlider.value = hpSlider.maxValue;
        enemiesLeftText.text = "Left: " + (enemyController.killsToWin - enemyController.enemyCount);
    }

    private void UpdateEnemyHP()
    {
        hpSlider.value -= enemyController.damagePerClick;
    }
}
