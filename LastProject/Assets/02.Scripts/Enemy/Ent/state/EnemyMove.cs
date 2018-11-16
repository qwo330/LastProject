public class EnemyMove : EnemyState 
{
    public override void EnterAction(abstractEnemy enemy)
    {
        enemy.animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, true);
        enemy.navMeshAgent.SetDestination(enemy.targetPlayerTransform.position);
        enemy.navMeshAgent.speed = enemy.currentSpeed;
        enemy.navMeshAgent.isStopped = false;
    }

    public override void ExitAction(abstractEnemy enemy)
    {
        throw new System.NotImplementedException();
    }
}
