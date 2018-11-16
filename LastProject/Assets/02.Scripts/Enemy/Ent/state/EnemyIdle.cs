public class EnemyIdle : EnemyState 
{
    public override void EnterAction(abstractEnemy enemy)
    {
        enemy.animatorComponent.SetBool(PlayerAniTrigger.ISIDLE, true);
    }

    public override void ExitAction(abstractEnemy enemy)
    {
        enemy.animatorComponent.SetBool(PlayerAniTrigger.ISIDLE, false);
    }
}