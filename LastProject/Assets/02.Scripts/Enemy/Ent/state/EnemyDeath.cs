public class EnemyDeath : EnemyState 
{
    public override void EnterAction(abstractEnemy enemy)
    {
        enemy.RemoveEnemy(enemy); //EnemySpawner에게 자신의 사망을 알림
        StageManager.Instance.player.GetExpAndGold(enemy.DropExp, enemy.DropGold);
        enemy.GiveItem();
        enemy.deadTimer.SetTimer(3f);
        enemy.deadTimer.StartTimer();
        enemy.navMeshAgent.isStopped = true;
        enemy.animatorComponent.SetBool(PlayerAniTrigger.DEATH, true);
    }

    public override void ExitAction(abstractEnemy enemy)
    {
        enemy.navMeshAgent.isStopped = false;
        enemy.animatorComponent.SetBool(PlayerAniTrigger.DEATH, false);
    }
}
