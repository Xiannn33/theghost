using UnityEngine;

public abstract class Commend
{
    public abstract void execute(GameObject go);
}
//public class MoveCommend : Commend
//{
//    Player player;
//    public override void execute(GameObject go)
//    {
//        player = go.GetComponent<Player>();
//        player.PlayerMove();
//    }

//}
//public class JumpCommend : Commend
//{
//    Player plaer;
//    public override void execute(GameObject go)
//    {
//        plaer = go.GetComponent<Player>();
//        plaer.PlayerJump();
//    }
//}