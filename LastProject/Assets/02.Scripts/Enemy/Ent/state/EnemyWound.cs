public class EnemyWound : EnemyState 
{
    public override void EnterAction(abstractEnemy enemy)
    {
        enemy.rigidbodyComponent.isKinematic = true;
        enemy.animatorComponent.SetBool(PlayerAniTrigger.WOUNDED, true);
    }

    public override void ExitAction(abstractEnemy enemy)
    {
        enemy.rigidbodyComponent.isKinematic = false;
        enemy.animatorComponent.SetBool(PlayerAniTrigger.WOUNDED, false);
    }
}
