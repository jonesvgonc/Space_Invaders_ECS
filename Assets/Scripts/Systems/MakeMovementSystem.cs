using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
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

        var speed = 0f;
        if (GetSingleton<GameDataComponent>().EnemyQuantity < 10) speed += 2f;
        else if (GetSingleton<GameDataComponent>().EnemyQuantity < 20) speed += 1.5f;
        else if (GetSingleton<GameDataComponent>().EnemyQuantity < 30) speed += 1f;
        else if (GetSingleton<GameDataComponent>().EnemyQuantity < 40) speed += 0.5f;
        var newmovement = new float3(0, 0, 0);

        Dependency = Entities
            .ForEach((EnemyComponent enemy, MoveDirectionComponent movement, ref Translation translation) =>
            {
                translation.Value += new float3(movement.MoveDirection.x * (enemy.EnemySpeed + speed) * delta, movement.MoveDirection.y * 1 * delta, 0);
            })
            .ScheduleParallel(Dependency);
    }
}
