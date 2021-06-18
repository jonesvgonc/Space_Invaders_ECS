using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class MakeMovementSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();

        RequireSingletonForUpdate<InGameFlag>();
    }

    protected override void OnUpdate()
    {
        var delta = Time.DeltaTime;

         Dependency = Entities
            .ForEach((PlayerComponent player, MoveDirectionComponent movement, ref Translation translation) =>
            {
                var newTranslate = translation.Value + movement.MoveDirection * player.CharacterSpeed * delta;

                var canMove = newTranslate.x < 5 &&
                    newTranslate.x > -5;

                if (canMove)
                {
                    translation.Value += movement.MoveDirection * player.CharacterSpeed * delta;
                }
            })
            .ScheduleParallel(Dependency);


        Dependency = Entities
            .ForEach((EnemyComponent enemy, MoveDirectionComponent movement, ref Translation translation) =>
            {
                translation.Value += movement.MoveDirection * enemy.EnemySpeed * delta;                
            })
            .ScheduleParallel(Dependency);
    }
}
