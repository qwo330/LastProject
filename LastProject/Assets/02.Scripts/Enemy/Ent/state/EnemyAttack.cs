public class EnemyAttack : EnemyState 
{
    public override void EnterAction(abstractEnemy enemy)
    {
        if (enemy.isAttackable)
        {
            enemy.navMeshAgent.isStopped = true;
            enemy.isAttackable = false;
            enemy.transform.LookAt(enemy.targetPlayerTransform);
            enemy.attackTimer.SetTimer(2f);
            enemy.attackTimer.StartTimer();
            enemy.rigidbodyComponent.isKinematic = true;
            enemy.animatorComponent.SetBool(PlayerAniTrigger.ATTACK, true);
        }
    }

    public override void ExitAction(abstractEnemy enemy)
    {
        enemy.rigidbodyComponent.isKinematic = false;
        enemy.animatorComponent.SetBool(PlayerAniTrigger.ATTACK, false);
    }
}
