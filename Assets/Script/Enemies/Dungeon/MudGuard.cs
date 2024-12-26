
public class MudGuard : EnemiesController
{
    //Slow Moving Speed
    protected override void Effects()
    {
        playerInstance.playerStats.isSlow = true;
    }
}
