using UnityEngine;

public class TurnBasedBattleManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private RPGCharacter player1;
    [SerializeField] private RPGCharacter player2;

    [Header("Turn Settings")]
    [SerializeField] private float turnDuration = 30f;

    private RPGCharacter currentPlayer;
    private RPGCharacter opponent;

    private float currentTimer;
    private bool turnActive;

    private void Start()
    {
        player1.OnCharacterDefeated += HandleGameOver;
        player2.OnCharacterDefeated += HandleGameOver;

        StartTurn(player1, player2);
    }

    private void Update()
    {
        if (!turnActive) return;

        currentTimer -= Time.deltaTime;

        if (currentTimer <= 0f)
        {
            EndTurn();
        }
    }

    private void StartTurn(RPGCharacter active, RPGCharacter target)
    {
        currentPlayer = active;
        opponent = target;

        currentPlayer.StartTurn();
        currentTimer = turnDuration;
        turnActive = true;

        Debug.Log($"{currentPlayer.name}'s Turn Started.");
    }

    public void PlayerNormalAttack()
    {
        if (!turnActive) return;

        if (currentPlayer.NormalAttack(opponent))
            EndTurn();
    }

    public void PlayerDefend()
    {
        if (!turnActive) return;

        if (currentPlayer.Defend())
            EndTurn();
    }

    public void PlayerUniqueSkill()
    {
        if (!turnActive) return;

        if (currentPlayer.UseUniqueSkill(opponent))
            EndTurn();
    }

    private void EndTurn()
    {
        turnActive = false;

        if (currentPlayer == player1)
            StartTurn(player2, player1);
        else
            StartTurn(player1, player2);
    }

    private void HandleGameOver(RPGCharacter defeated)
    {
        turnActive = false;
        Debug.Log($"{defeated.name} has been defeated!");
    }
}
